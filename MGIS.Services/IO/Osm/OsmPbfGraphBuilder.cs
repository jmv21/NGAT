﻿using NGAT.Business;
using NGAT.Business.Contracts.IO;
using NGAT.Business.Domain.Core;
using NGAT.Geo;
using NGAT.Geo.Geometries;
using OsmSharp;
using OsmSharp.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGAT.Services.IO.Osm
{
    public class OsmPbfGraphBuilder : IGraphBuilder
    {
        public OsmPbfGraphBuilder(IAttributeFilterCollection linkFilters,
            IAttributesFetcherCollection nodeAttrsFetchers,
            IAttributesFetcherCollection linkAttrsFetechers)
        {

            LinkFilters = linkFilters;
            NodeAttributesFetchers = nodeAttrsFetchers;
            LinkAttributesFetchers = linkAttrsFetechers;
        }

        #region IGraphBuilderMembers
        public Uri DigitalMapURI { get; set; }

        public IAttributeFilterCollection LinkFilters { get; set; }

        public IAttributesFetcherCollection NodeAttributesFetchers { get; set; }

        public IAttributesFetcherCollection LinkAttributesFetchers { get; set; }

        public bool Pedestrian { get; set; }

        public string DigitalMapFormatID => "PBF"; 


        public Graph Build(Uri fileURI)
        {
            return InternalBuild(fileURI);
        }

        public Task<Graph> BuildAsync(Uri fileURI)
        {
            return new Task<Graph>(() => InternalBuild(fileURI));
        }

        public Graph BuildInRegion(Uri fileUri, Polygon region)
        {
            var coordinateSystem = new GeoCoordinateSystem();
            var graph = Build(fileUri);
            Graph subGraph = graph.SubGraphInRegion(region, coordinateSystem);
            return subGraph;
        }
        #endregion

        /// <summary>
        /// Builds the graph from <paramref name="input"/>
        /// </summary>
        /// <param name="input">Input for this GraphBuilder</param>
        /// <returns></returns>
        private Graph InternalBuild(Uri pbfFileURI)
        {
            DigitalMapURI = pbfFileURI;
            if (!System.IO.File.Exists(DigitalMapURI.LocalPath))
                throw new ArgumentException("Pbf file specified is invalid or doesn't exists.");

            Graph network = new Graph();

            var nodeToVertex = new SortedDictionary<long, int>(); //mapping from osm nodes to network's vertexes
            SortedDictionary<long, int> nodesLinkCounter = new SortedDictionary<long, int>(); //the osm nodes with a link counter > 0 will become a network vertex
            List<Way> whiteListedWays = new List<Way>(); //the osm ways that will become links between network's vertexes

            using (var fileStream = System.IO.File.OpenRead(DigitalMapURI.LocalPath))// DEFAULT: File.OpenRead(DigitalMapURI.LocalPath)
            {
                var streamSource = new PBFOsmStreamSource(fileStream);

                #region Whitelisting links and nodes
                foreach (Way way in streamSource.Where(o => o.Type == OsmGeoType.Way))
                {
                    var wayAttrs = way.Tags.ToDictionary(t => t.Key, t => t.Value);
                    if (LinkFilters.ApplyAllFilters(wayAttrs))
                    {
                        whiteListedWays.Add(way);
                        //Whitelisting first Node
                        if (nodesLinkCounter.ContainsKey(way.Nodes[0]))
                            nodesLinkCounter[way.Nodes[0]] += 1;
                        else
                            nodesLinkCounter[way.Nodes[0]] = 1;

                        for (int i = 1; i < way.Nodes.Length - 1; i++)
                        {
                            if (nodesLinkCounter.ContainsKey(way.Nodes[i]))
                                nodesLinkCounter[way.Nodes[i]] += 1;
                            else
                                nodesLinkCounter[way.Nodes[i]] = 0;
                        }

                        //Whitelisting last Node
                        if (nodesLinkCounter.ContainsKey(way.Nodes[way.Nodes.Length - 1]))
                            nodesLinkCounter[way.Nodes[way.Nodes.Length - 1]] += 1;
                        else
                            nodesLinkCounter[way.Nodes[way.Nodes.Length - 1]] = 1;
                    }
                }

                var notAddedNodes = new SortedDictionary<long, OsmSharp.Node>();
                #endregion

                streamSource.Reset();

                #region Adding Relevant Nodes
                foreach (OsmSharp.Node osmNode in streamSource.Where(o => o.Type == OsmGeoType.Node))
                {
                    #region Reading Node Attributes
                    IDictionary<string, string> attributes = new Dictionary<string, string>();
                    foreach (var tag in osmNode.Tags)
                    {
                        attributes.Add(tag.Key, tag.Value);
                    }
                    #endregion


                    #region Filtering node by its link counters
                    if (osmNode.Longitude.HasValue
                        && osmNode.Latitude.HasValue
                        && osmNode.Id.HasValue
                        && nodesLinkCounter.ContainsKey(osmNode.Id.Value)
                        && nodesLinkCounter[osmNode.Id.Value] > 0)
                    {
                        //The node has more than one way that traverses it so we add it to the network
                        var newNode = new Business.Domain.Core.Node()
                        {
                            Latitude = osmNode.Latitude.Value,
                            Longitude = osmNode.Longitude.Value,
                        };

                        //Fetching node attributes
                        var fecthedAttributes = NodeAttributesFetchers.Fetch(attributes);

                        //Adding the node to the graph
                        network.AddNode(newNode, fecthedAttributes);

                        nodeToVertex.Add(osmNode.Id.Value, newNode.Id);
                    }
                    else if (osmNode.Longitude.HasValue && osmNode.Latitude.HasValue && osmNode.Id.HasValue)//Conditions for a node to be valid
                    {
                        //We store discarded nodes (with link counter=0) for accuracy in distance calculations
                        notAddedNodes.Add(osmNode.Id.Value, osmNode);
                    }
                    #endregion
                }
                #endregion

                #region Adding Arcs

                foreach (Way osmWay in whiteListedWays)
                {
                    #region Reading way attributes
                    IDictionary<string, string> attributes = new Dictionary<string, string>();
                    foreach (var tag in osmWay.Tags)
                    {
                        attributes.Add(tag.Key, tag.Value);
                    }
                    #endregion

                    #region Filtering ways by its attributes
                    //Way already passed all filters so we process it and fetch the attributes needed
                    var fetchedArcAttributes = LinkAttributesFetchers.Fetch(attributes);
                    var arcData = new LinkData()
                    {
                        RawData = Newtonsoft.Json.JsonConvert.SerializeObject(fetchedArcAttributes)
                        
                    };
                    
                    network.AddLinkData(arcData);
                    //Determinig if this way is one-way and if it is, determining it direction
                    bool oneWay = !Pedestrian && (attributes.ContainsKey("oneway")
                        && attributes["oneway"].ToLowerInvariant() != "no"
                        && attributes["oneway"].ToLowerInvariant() != "0"
                        && attributes["oneway"].ToLowerInvariant() != "false")
                        ||
                        (attributes.ContainsKey("junction")
                        && (attributes["junction"].ToLowerInvariant() == "circular" || attributes["junction"].ToLowerInvariant() == "roundabout"))
                        ||
                        (attributes.ContainsKey("highway")
                        && ((attributes["highway"].ToLowerInvariant() == "motorway")
                            ||
                            (attributes["highway"].ToLowerInvariant() == "motorway_link")
                            ||
                            (attributes["highway"].ToLowerInvariant() == "mini_roundabout")
                        ));

                    bool forwardDirection = attributes.ContainsKey("oneway") && (attributes["oneway"].ToLowerInvariant() == "yes"
                        || attributes["oneway"].ToLowerInvariant() == "1"
                        || attributes["oneway"].ToLowerInvariant() == "true");

                    ProcessWay(network, osmWay, oneWay, oneWay ? forwardDirection : true, arcData, nodeToVertex, notAddedNodes);



                    #endregion

                }
                #endregion

            }

            return network;
        }

        /// <summary>
        /// Process a way according to its direction
        /// </summary>
        /// <param name="result">The graph being build.</param>
        /// <param name="osmWay">The OSM way to read nodes from</param>
        /// <param name="oneway">A value indicating if this way is one way</param>
        /// <param name="forward">A value indicating if the processing should be made in the same order as the way</param>
        /// <param name="arcData">The fetched arc attributes for this way</param>
        /// <param name="notAddedNodes">The nodes that didn't pass the filters, bu might still be part of a way, necessary for distance calculations.</param>
        private void ProcessWay(Graph result, OsmSharp.Way osmWay, bool oneway, bool forward, LinkData arcData, IDictionary<long, int> nodeToVertexMapping, IDictionary<long, OsmSharp.Node> notAddedNodes)
        {
            var fromNodeId = forward ? osmWay.Nodes[0] : osmWay.Nodes[osmWay.Nodes.Length - 1];
            var initialIterator = forward ? 1 : osmWay.Nodes.Length - 2;
            int iteratorModifier = forward ? 1 : -1;

            for (int i = initialIterator; forward ? i < osmWay.Nodes.Length : i >= 0; i += iteratorModifier)
            {
                var toNodeId = osmWay.Nodes[i];

                if (nodeToVertexMapping.ContainsKey(toNodeId))
                {
                    //Both nodes were added to the graph, so we process the arc
                    result.AddLink(nodeToVertexMapping[fromNodeId], nodeToVertexMapping[toNodeId], arcData, oneway);

                }
                else
                {
                    //The originNode was stored, but not the destination, so we iterate trhough the way untill a stored node is found
                    double accumulatedDistance = 0;
                    var fromNode = result.NodesId[nodeToVertexMapping[fromNodeId]];

                    //First node
                    List<Tuple<double, double, long>> intermediatePoints = new List<Tuple<double, double, long>>() { new Tuple<double, double, long>(fromNode.Latitude, fromNode.Longitude, 0) };

                    while (notAddedNodes.TryGetValue(toNodeId, out OsmSharp.Node toNode))
                    {
                        accumulatedDistance += fromNode.Coordinate.GetDistanceTo(new Point2D(toNode.Latitude.Value, toNode.Longitude.Value) { CoordinateSystem = Point2D.GeoCoordinateSystem });
                        intermediatePoints.Add(new Tuple<double, double, long>(toNode.Latitude.Value, toNode.Longitude.Value, toNode.Id.Value));
                        i += iteratorModifier;
                        toNodeId = osmWay.Nodes[i];
                    }

                    //Last node
                    intermediatePoints.Add(new Tuple<double, double, long>(result.NodesId[nodeToVertexMapping[toNodeId]].Latitude, result.NodesId[nodeToVertexMapping[toNodeId]].Longitude, 0));

                    if (intermediatePoints[0].Equals(intermediatePoints[intermediatePoints.Count - 1]) && intermediatePoints.Count > 2)
                    {
                        //Last Node equals first, so we split the edge for avoiding loops, we use the first not added node of the way
                        var toAddNode = notAddedNodes[intermediatePoints[1].Item3];
                        notAddedNodes.Remove(intermediatePoints[1].Item3);

                        //Fetching new node attributes
                        var attributes = NodeAttributesFetchers.Fetch(toAddNode.Tags.ToDictionary(t => t.Key, t => t.Value));

                        var newNode = new Business.Domain.Core.Node()
                        {
                            Latitude = toAddNode.Latitude.Value,
                            Longitude = toAddNode.Longitude.Value
                        };
                        result.AddNode(newNode, attributes);
                        nodeToVertexMapping[toAddNode.Id.Value] = newNode.Id;

                        toNodeId = toAddNode.Id.Value;
                        //Adding link between first node and second and continue processing
                        result.AddLink(nodeToVertexMapping[fromNodeId], newNode.Id, arcData, oneway);

                    }
                    else if (!intermediatePoints[0].Equals(intermediatePoints[intermediatePoints.Count - 1]))
                    {
                        //There was no problem, we add the full link
                        result.AddLink(nodeToVertexMapping[fromNodeId], nodeToVertexMapping[toNodeId], accumulatedDistance, arcData, oneway, intermediatePoints.Select(t => new Point2D(t.Item1, t.Item2)));
                    }
                    //Other cases indicate broken ways
                }
                fromNodeId = toNodeId;
            }

        }
    }

}
