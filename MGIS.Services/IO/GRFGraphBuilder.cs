using NGAT.Business;
using NGAT.Business.Contracts.IO;
using NGAT.Business.Domain.Core;
using NGAT.Geo;
using NGAT.Geo.Geometries;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NGAT.Services.IO
{
    public class GRFGraphBuilder : IGraphBuilder
    {
        public Uri DigitalMapURI { get; set; }
        public IAttributeFilterCollection LinkFilters { get; set; }
        public IAttributesFetcherCollection NodeAttributesFetchers { get; set; }
        public IAttributesFetcherCollection LinkAttributesFetchers { get; set; }
        public bool Pedestrian { get; set; }

        public string DigitalMapFormatID => "GRF";

        public Graph Build(Uri fileUri)
        {
            DigitalMapURI = fileUri;
            if (!System.IO.File.Exists(DigitalMapURI.LocalPath))
                throw new ArgumentException("GRF file specified is invalid or doesn't exists.");

            var network = new Graph();

            using (var stream = System.IO.File.OpenRead(DigitalMapURI.LocalPath))
            {
                using (var tr = new StreamReader(stream))
                {
                    tr.ReadLine();//GRAFO
                    tr.ReadLine();//type

                    string nodeString = null;
                    var fakeAttr = new Dictionary<string, string>();
                    var fakeLinkData = new LinkData() { RawData = "{}"};
                    while ((nodeString = tr.ReadLine()) != null)
                    {
                        var nodeSplitted = nodeString.Split(' ');
                        


                        if (!int.TryParse(nodeSplitted[0], out int nodeId)
                            || !double.TryParse(nodeSplitted[1].Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out double latitude)
                            || !double.TryParse(nodeSplitted[2].Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out double longitude)
                            || !int.TryParse(nodeSplitted[3], out int last))
                            throw new Exception("GRF File in an incorrect format");

                        else
                        {
                            network.AddNode(new Business.Domain.Core.Node()
                            {
                                Id = nodeId,
                                Latitude = latitude,
                                Longitude = longitude
                            }, fakeAttr);
                            if (last == 1)
                                break;
                        }


                    }

                    string linkString = null;
                    while ((linkString = tr.ReadLine()) != null)
                    {
                        var linkSplitted = linkString.Split(' ');
                        if (!(linkSplitted.Length >= 7)
                            || !int.TryParse(linkSplitted[0], out int fromId)
                            || !int.TryParse(linkSplitted[1], out int toId)
                            || !double.TryParse(linkSplitted[2].Replace(',','.'),NumberStyles.Float, CultureInfo.InvariantCulture, out double distance)
                            || !int.TryParse(linkSplitted[3], out int linkType)
                            || !int.TryParse(linkSplitted[4], out int isMultiScenario)
                            || !int.TryParse(linkSplitted[5], out int scenariosCount)
                            || !(linkType == 0 || linkType == 1)
                            || !(isMultiScenario == 0 || isMultiScenario == 1))
                            throw new Exception("GRF File in a bad format");

                        Distance distances = new Distance(new List<double>(),false);
                        if (isMultiScenario == 1)
                        {
                            List<Double> costs = new List<Double>();
                            for (int i = 7; i < 7 + scenariosCount; i++)
                            {
                                costs.Add(double.Parse(linkSplitted[i]));
                            }
                            distances = new Distance(costs,true);
                        }

                        int index = isMultiScenario == 1 ? 7 + scenariosCount : 7;

                        if (linkSplitted.Length > index ) //GRF File with Data
                        {
                            string rawData = "";
                            for (int i = index; i < linkSplitted.Length; i++)
                            {
                                rawData = rawData + linkSplitted[i];
                            }
                            fakeLinkData = new LinkData() { RawData=rawData };
                        }
                        if (isMultiScenario == 0) 
                        {
                        network.AddLink(fromId, toId, distance, fakeLinkData, linkType == 0 && !Pedestrian);
                        }
                        else 
                        {
                            network.AddLink(fromId, toId, distances, fakeLinkData, linkType == 0 && !Pedestrian);
                        }

                    }
                }
            }
            return network;
        }

        public Task<Graph> BuildAsync(Uri fileUri)
        {
            return new Task<Graph>(() => Build(fileUri));
        }

        public Graph BuildInRegion(Uri fileUri, Polygon region)
        {
            var coordinateSystem = new GeoCoordinateSystem();
            DigitalMapURI = fileUri;
            if (!System.IO.File.Exists(DigitalMapURI.LocalPath))
                throw new ArgumentException("GRF file specified is invalid or doesn't exists.");

            var network = new Graph();

            using (var stream = System.IO.File.OpenRead(DigitalMapURI.LocalPath))
            {
                using (var tr = new StreamReader(stream))
                {
                    tr.ReadLine();//GRAFO
                    tr.ReadLine();//type

                    string nodeString = null;
                    var fakeAttr = new Dictionary<string, string>();
                    var fakeLinkData = new LinkData();
                    while ((nodeString = tr.ReadLine()) != null)
                    {
                        var nodeSplitted = nodeString.Split(' ');

                        if (!int.TryParse(nodeSplitted[0], out int nodeId)
                            || !double.TryParse(nodeSplitted[1], out double latitude)
                            || !double.TryParse(nodeSplitted[2], out double longitude)
                            || !int.TryParse(nodeSplitted[3], out int last))
                            throw new Exception("GRF File in an incorrect format");

                        else
                        {
                            var node = new Business.Domain.Core.Node()
                            {
                                Id = nodeId,
                                Latitude = latitude,
                                Longitude = longitude,

                            };
                            var intersection = node.ToPoint2D(coordinateSystem).Intersection(region, out Location loc);
                            if (intersection != null && (loc == Location.Boundary || loc == Location.Interior))
                            {
                                network.AddNode(node, fakeAttr);
                                if (last == 1)
                                    break;
                            }
                        }


                    }

                    string linkString = null;
                    while ((linkString = tr.ReadLine()) != null)
                    {
                        var linkSplitted = linkString.Split(' ');
                        if (!(linkSplitted.Length == 7)
                            || !int.TryParse(linkSplitted[0], out int fromId)
                            || !int.TryParse(linkSplitted[1], out int toId)
                            || !double.TryParse(linkSplitted[2], out double distance)
                            || !int.TryParse(linkSplitted[3], out int linkType)
                            || !(linkType == 0 || linkType == 1))
                            throw new Exception("GRF File in a bad format");

                        else
                        {
                            var fromNode = network.NodesId[fromId];
                            var toNode = network.NodesId[toId];
                            var line2D = new Line2D(fromNode.ToPoint2D(coordinateSystem), toNode.ToPoint2D(coordinateSystem));
                            var intersection = line2D.Intersection(region, out Location loc);

                            if (intersection != null && loc == Location.Interior)
                                network.AddLink(fromId, toId, distance, fakeLinkData, linkType == 0 && !Pedestrian);
                        }

                    }
                }
            }
            return network;

        }
    }

}
