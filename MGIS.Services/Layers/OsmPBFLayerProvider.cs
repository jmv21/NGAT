using System;
using System.Collections.Generic;
using System.Linq;
using NGAT.Geo.Geometries;
using NGAT.Business.Contracts.Services.Layers;
using NGAT.Business.Domain.Core;
using OsmSharp;
using OsmSharp.Streams;

namespace NGAT.Services.Layers
{
    public class OsmPBFLayerProvider : FileLayerProvider
    {
        public OsmPBFLayerProvider() : base("PBF")
        {
        }

        /// <summary>
        /// Gets a layer from a shape file.
        /// </summary>
        /// <param name="dataSource">The path of a PBF file.</param>
        /// <param name="filter">An Osm PBF filter</param>
        /// <param name="layerName">The name of the resulting layer</param>
        /// <param name="iconPath">The icon's path of the layer.</param>
        /// <returns></returns>
        public override Layer GetLayer(string dataSource, ILayerFilter filter, string layerName, string iconPath)
        {
            if (!GetType().IsSubclassOf(filter.ProviderType) && filter.ProviderType != this.GetType())
                throw new ArgumentException("The selected filter does not match the Osm PBF Layer Provider.");

            List<Geo.Contracts.IGeometry> geometries = new List<Geo.Contracts.IGeometry>();
            using (var fileStream = System.IO.File.OpenRead(dataSource))
            {
                var source = new PBFOsmStreamSource(fileStream);

                foreach (var feature in source.Where(f => filter.Match(f)))
                {
                    switch (feature.Type)
                    {
                        case OsmGeoType.Node:
                            #region Processing Nodes     
                            var node = feature as OsmSharp.Node;
                            if (node.Latitude != null && node.Longitude != null)
                            {
                                Point2D point = new Point2D((double)node.Latitude, (double)node.Longitude);
                                geometries.Add(point);
                            }

                            #endregion
                            break;     
                        case OsmGeoType.Way:
                            #region Processing Ways

                            var way = feature as Way;
                            List<Geo.Geometries.Coordinate> coords = new List<Geo.Geometries.Coordinate>();
                            foreach (var nodeId in way.Nodes)
                                foreach (OsmSharp.Node nod in source.Where(n => n.Id == nodeId))
                                {
                                    if (nod.Latitude != null && nod.Longitude != null)
                                    {
                                        coords.Add(new Geo.Geometries.Coordinate((double)nod.Latitude, (double)nod.Longitude));
                                    }
                                }
                            Geo.Geometries.LineString lineString = new Geo.Geometries.LineString(coords.ToArray());
                            geometries.Add(lineString);

                            #endregion
                            break;
                        case OsmGeoType.Relation:
                            #region Processing Relations

                            var relation = feature as Relation;
                            foreach (var member in relation.Members)
                            {
                                if(member.Type == OsmGeoType.Node)
                                {
                                    foreach (OsmSharp.Node nod in source.Where(n => n.Id == member.Id))
                                    {
                                        if (nod.Latitude != null && nod.Longitude != null)
                                        {
                                            Point2D point = new Point2D((double)nod.Latitude, (double)nod.Longitude);
                                            geometries.Add(point);
                                        }
                                    }
                                }
                                else if(member.Type == OsmGeoType.Way)
                                {
                                    foreach (Way way1 in source.Where(w => w.Id == member.Id))
                                    {
                                        coords = new List<Geo.Geometries.Coordinate>();
                                        foreach (var nodeId in way1.Nodes)
                                            foreach (OsmSharp.Node nod in source.Where(n => n.Id == nodeId))
                                            {
                                                if (nod.Latitude != null && nod.Longitude != null)
                                                {
                                                    coords.Add(new Geo.Geometries.Coordinate((double)nod.Latitude, (double)nod.Longitude));
                                                }
                                            }
                                        lineString = new Geo.Geometries.LineString(coords.ToArray());
                                        geometries.Add(lineString);
                                    }
                                }
                            }

                            #endregion
                            break;
                        default:
                            break;
                    }
                }
            }

            return new Layer(layerName, iconPath, geometries);
        }

        public override string ToString()
        {
            return "Osm PBF Provider";
        }
    }
}
