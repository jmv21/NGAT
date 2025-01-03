using NGAT.Business.Domain.Base;
using NGAT.Geo;
using NGAT.Geo.Geometries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NGAT.Business.Domain.Core
{
    public class Graph : Entity, ICloneable
    {
        public Graph()
        {
            ArcDatas = new List<LinkData>();
            NodesId = new SortedDictionary<int, Node>();
            Nodes = new List<Node>();
            Arcs = new List<Arc>();
            Edges = new List<Edge>();
            TurnProhibitions = new List<(Node, Node, Node)>();
        }

        #region Properties

        public string Name { get; set; }
        /// <summary>
        /// Maps an ID to its respective node. A node's Id doesn't match its position.
        /// </summary>
        [JsonIgnore]
        public virtual IDictionary<int, Node> NodesId { get; set; }
        /// <summary>
        /// The turn prohibitions used for SP calculations
        /// </summary>
        public List<(Node,Node,Node)> TurnProhibitions { get; set; }
        /// <summary>
        /// The nodes of this graph
        /// </summary>
        public int Scenarios_Count { get; set; }
        public virtual IList<Node> Nodes { get; set; }

        public void CompleteBuild()
        {
            //Create node index
            NodesId = new SortedDictionary<int, Node>(Nodes.ToDictionary(n => n.Id));

            //Remove incomplete edges or arcs
            foreach (Link toDeleteLink in Arcs.Concat<Link>(Edges).ToArray())
            {
                if (!NodesId.ContainsKey(toDeleteLink.FromNodeId) || !NodesId.ContainsKey(toDeleteLink.ToNodeId))
                {
                    if (toDeleteLink.Directed)
                        Arcs.Remove(toDeleteLink as Arc);
                    else
                        Edges.Remove(toDeleteLink as Edge);

                }
            }

            //Complete relations
            foreach (var edge in Edges)
            {
                var fromNode = NodesId[edge.FromNodeId];
                var toNode = NodesId[edge.ToNodeId];
                edge.FromNode = fromNode;
                edge.ToNode = toNode;
                if (!fromNode.Edges.Contains(edge))
                    fromNode.Edges.Add(edge);
                if (!toNode.Edges.Contains(edge))
                    fromNode.Edges.Add(edge);
            }
            foreach (var arc in Arcs)
            {
                var fromNode = NodesId[arc.FromNodeId];
                var toNode = NodesId[arc.ToNodeId];
                arc.FromNode = fromNode;
                arc.ToNode = toNode;
                if (!fromNode.OutgoingArcs.Contains(arc))
                    fromNode.OutgoingArcs.Add(arc);
                if (!toNode.IncomingArcs.Contains(arc))
                    fromNode.IncomingArcs.Add(arc);
            }
        }

        public Graph SubGraphInRegion(double minLat, double minLon, double maxLat, double maxLon)
        {
            var nodes = Nodes.Where(n => n.Latitude >= minLat && n.Longitude >= minLon && n.Latitude <= maxLat && n.Longitude <= maxLon);
            var graph = new Graph();

            foreach (var node in nodes)
            {
                graph.AddNode(new Node { Id = node.Id, Latitude = node.Latitude, Longitude = node.Longitude }, node.NodeAttributes);


            }
            Dictionary<int, bool> markedEdges = new Dictionary<int, bool>();
            foreach (var node in nodes)
            {
                foreach (var edge in node.Edges)
                {
                    if (graph.NodesId.ContainsKey(edge.FromNodeId)
                        && graph.NodesId.ContainsKey(edge.FromNodeId)
                        && !markedEdges.ContainsKey(edge.Id))
                    {
                        markedEdges[edge.Id] = true;
                        graph.AddLink(edge.FromNodeId, edge.ToNodeId, edge.LinkData, false);
                    }

                }
                foreach (var arc in node.OutgoingArcs)
                {
                    if (graph.NodesId.ContainsKey(arc.ToNodeId))
                        graph.AddLink(arc.FromNodeId, arc.ToNodeId, arc.LinkData, true);
                }
            }

            graph.CompleteBuild();
            return graph;
        }

        /// <summary>
        /// The Arcs of this graph
        /// </summary>
        public virtual IList<Arc> Arcs { get; set; }

        /// <summary>
        /// The Arcs profiles for this graph
        /// </summary>
        public virtual IList<LinkData> ArcDatas { get; set; }

        /// <summary>
        /// The Edges of this Graph
        /// </summary>
        public virtual IList<Edge> Edges { get; set; }

        /// <summary>
        /// The Links of this Graph
        /// </summary>
        public IEnumerable<Link> Links
        {
            get
            {
                foreach (var edge in Edges)
                    yield return edge;

                foreach (var arc in Arcs)
                    yield return arc;
            }
        }
        
        /// <summary>
        /// Maximum ID of any node
        /// </summary>
        public int NodesMaxId { get; private set; }

        /// <summary>
        /// Maximum ID of any edge
        /// </summary>
        public int EdgesMaxId { get; private set; }

        /// <summary>
        /// Maximum ID of any arc
        /// </summary>
        public int ArcsMaxId { get; private set; }

        #endregion


        #region Methods

        #region Nodes
        /// <summary>
        /// Adds a node to this graph
        /// </summary>
        /// <param name="node">The node to add</param>
        /// <param name="originalId">The original Id of the node object in its original data source</param>
        /// <param name="fetchedAttributes">The attributes to stores for this node</param>
        /// <returns>ID of the inserted node</returns>
        public int AddNode(Node node, IDictionary<string, string> fetchedAttributes)
        {

            //Using the well-formed hash code of the lexicographical coordinate
            if (node.Id > 0)
                NodesMaxId = Math.Max(NodesMaxId, node.Id);
            else node.Id = node.Id > 0 ? node.Id : ++NodesMaxId;// node.Coordinate.GetHashCode(); this is not WORKING

            //Converting the fetched attributes for the node to Json and storing it
            if (fetchedAttributes != null)
                node.NodeData = JsonConvert.SerializeObject(fetchedAttributes);

            //Storing the mapping
            //this.VertexToNodesIndex.Add(originalId, node.Id);

            //Saving the node in collection and index
            NodesId.Add(node.Id, node);
            Nodes.Add(node);
            return node.Id;
        }

        /// <summary>
        /// Adds a node to this graph
        /// </summary>
        /// <param name="latitude">The latitude of the node</param>
        /// <param name="longitude">The longitude of the node</param>
        /// <param name="originalId">The original Id of the node object in its original data source</param>
        /// <param name="fetchedAttributes">The attributes to stores for this node</param>
        public int AddNode(double latitude, double longitude, IDictionary<string, string> fetchedAttributes)
        {
            return AddNode(new Node
            {
                Latitude = latitude,
                Longitude = longitude
            }, fetchedAttributes);
        }

        /// <summary>
        /// Inerts a node into a given link. The link is partitioned in two new links
        /// </summary>
        /// <param name="node">The node to be inserted</param>
        /// <param name="link">The link to be partitioned</param>
        /// <param name="fetchedAttributes">The attributes to stores for this node</param>
        /// <returns>The ID of the inserted node</returns>
        public int InsertNode(Node node, Link link, IDictionary<string, string> fetchedAttributes)
        {
            var fromNode = link.FromNode;
            var toNode = link.ToNode;

            //foreach ((Node,Node,Node) tp in TurnProhibitions)//Checking if a node is inserted in a Turn Prohibition
            //{
            //    if (((fromNode == tp.Item1) && (toNode == tp.Item2)) || ((!link.Directed) && (fromNode == tp.Item2) && (toNode == tp.Item1)))
            //        TurnProhibitions.Add((node, tp.Item2, tp.Item3));

            //    if (((fromNode == tp.Item2) && (toNode == tp.Item3)) || ((!link.Directed) && (fromNode == tp.Item3) && (toNode == tp.Item2)))
            //        TurnProhibitions.Add((tp.Item1, tp.Item2, node));
            //}

            DeleteLink(link);
            AddNode(node, fetchedAttributes);
            AddLink(fromNode, node, link.LinkData, link.Directed);
            AddLink(node, toNode, link.LinkData, link.Directed);
            return node.Id;
        }

        /// <summary>
        /// Inerts a node into a given link. The link is partitioned in two new links
        /// </summary>
        /// <param name="latitude">The latitude of the node</param>
        /// <param name="longitude">The longitude of the node</param>
        /// <param name="link">The link to be partitioned</param>
        /// <param name="fetchedAttributes">The attributes to stores for this node</param>
        /// <returns>The ID of the inserted node</returns>
        public int InsertNode(double latitude, double longitude, Link link, IDictionary<string, string> fetchedAttributes)
        {
            var node = new Node()
            {
                Latitude = latitude,
                Longitude = longitude
            };
            return InsertNode(node, link, fetchedAttributes);
        }

        /// <summary>
        /// Inserts the node that is the projection of the given point with its closer link in the graph.
        /// </summary>
        /// <param name="latitude">The latitude of the point</param>
        /// <param name="longitude">The longitude of the point</param>
        /// <returns>The ID of the inserted node</returns>
        public int InsertPoint(double latitude, double longitude)
        {
            // find the nearest node
            PointLatLong point = new PointLatLong(latitude, longitude);
            Node nearestNode = NodesId[GetNearestNode(latitude, longitude)];

            if (nearestNode.Links.Count() == 0)
                throw new Exception("Graph is disconnected!");

            // Find the closer link to the point and its instersection
            GeoLine pointToNode = new GeoLine(point, nearestNode.Coordinate); // Line from point to nearestnode
            GeoLine perpendicular = new GeoLine(nearestNode.Coordinate, -1 / pointToNode.M); // the perpendicular that passes through the node
            Link closerLink = null; // the closer link
            PointLatLong projection = null; // the projection of point on the closer link
            double distance = double.PositiveInfinity;

            if(point >= perpendicular) // The point is above the line
            {
                foreach (var link in nearestNode.Links.Where(l => (l.FromNode.Coordinate >= perpendicular && l.ToNode.Coordinate >= perpendicular)))
                {                    
                    GeoLine linkLine = new GeoLine(link.FromNode.Coordinate, link.ToNode.Coordinate); // the line that contains the link
                    GeoLine pointToLinkLine = new GeoLine(point, -1 / linkLine.M); // perpendicular that passes through the  point
                    Location location = Location.Null;
                    var intersection = (Coordinate)linkLine.Intersection(pointToLinkLine, out location);
                    if (location != Location.Partial)
                        throw new InvalidOperationException("Lines do not cut each other.");
                    double d = point.GetDistanceTo(intersection);
                    if(d < distance)
                    {
                        distance = d;
                        closerLink = link;
                        projection = new PointLatLong(intersection[0], intersection[1]);
                    }

                }
            }
            else // the point is below the line
            {
                foreach (var link in nearestNode.Links.Where(l => (l.FromNode.Coordinate <= perpendicular && l.ToNode.Coordinate <= perpendicular)))
                {                    
                    GeoLine linkLine = new GeoLine(link.FromNode.Coordinate, link.ToNode.Coordinate); // the line that contains the link
                    GeoLine pointToLinkLine = new GeoLine(point, -1 / linkLine.M); // perpendicular that passes through the  point
                    Location location = Location.Null;
                    var intersection = (Coordinate)linkLine.Intersection(pointToLinkLine, out location);
                    if (location != Location.Partial)
                        throw new InvalidOperationException("Lines does not cut each other.");
                    double d = point.GetDistanceTo(intersection);
                    if (d < distance)
                    {
                        distance = d;
                        closerLink = link;
                        projection = new PointLatLong(intersection[0], intersection[1]);
                    }
                }
            }
            
            if (closerLink == null || projection == null) // There is no link to be particioned, a new link must be added
            {
                Node node = new Node() { Latitude = latitude, Longitude = longitude };
                AddNode(node, new Dictionary<string, string>());
                AddLink(node, nearestNode, new LinkData(), false);
                return node.Id;
            }
            // The node to insert is projection and the link to be partiontioned is closerlink
            return InsertNode(projection.Latitude, projection.Longitude, closerLink, new Dictionary<string, string>());
        }

        /// <summary>
        /// Deletes a node from the graph.
        /// </summary>
        /// <param name="nodeToDelete">The node to delete.</param>
        public void DeleteNode(Node nodeToDelete)
        {
            foreach (var edge in nodeToDelete.Edges)
            {
                if (edge.FromNode == nodeToDelete)
                    edge.ToNode.Edges.Remove(edge);
                else
                    edge.FromNode.Edges.Remove(edge);

                Edges.Remove(edge);
            }
            foreach (var arc in nodeToDelete.IncomingArcs)
            {
                arc.FromNode.OutgoingArcs.Remove(arc);
                Arcs.Remove(arc);
            }
            foreach (var arc in nodeToDelete.OutgoingArcs)
            {
                arc.ToNode.IncomingArcs.Remove(arc);
                Arcs.Remove(arc);
            }
            Nodes.Remove(nodeToDelete);
            NodesId.Remove(nodeToDelete.Id);
        }

        /// <summary>
        /// Gets the nearest node to a given location.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns>The ID of the nearest node.</returns>
        public int GetNearestNode(double latitude, double longitude)
        {
            Node fromNode = new Node() { Latitude = latitude, Longitude = longitude };
            double distance = double.PositiveInfinity;
            Node toNode = null;

            foreach (var node in Nodes)
            {
                double dist = fromNode.Coordinate.GetDistanceTo(node.Coordinate);
                if( dist < distance)
                {
                    distance = dist;
                    toNode = node;
                }
            }

            return toNode.Id;
        }



        #endregion

        #region Links
        /// <summary>
        /// Adds an arc to the graph and calculates distance between nodes coordinates
        /// </summary>
        /// <param name="fromOriginalNodeId">The Id of the origin point from data source</param>
        /// <param name="toOriginalNodeId">The Id of the destination point from data source</param>
        /// <param name="fetchedArcAttributes">The attributes to store for this arc</param>
        public void AddLink(int fromNodeId, int toNodeId, LinkData linkData, bool directed)
        {
            var fromNode = NodesId[fromNodeId];
            var toNode = NodesId[toNodeId];
            var distance = fromNode.Coordinate.GetDistanceTo(toNode.Coordinate);
            AddLink(fromNode, toNode, distance, linkData, directed);
        }

        /// <summary>
        /// Adds an arc to the graph with the provided distance (for use when real distance is not the distance between coordinates)
        /// </summary>
        /// <param name="fromOriginalNodeId">The Id of the origin point from data source</param>
        /// <param name="toOriginalNodeId">The Id of the destination point from data source</param>
        /// <param name="distance">Provided distance</param>
        /// <param name="fetchedArcAttributes">The attributes to store for this arc</param>
        public void AddLink(int fromNodeId, int toNodeId, double distance, LinkData linkData, bool directed, IEnumerable<Point2D> coordinates = null)
        {
            var fromNode = NodesId[fromNodeId];
            var toNode = NodesId[toNodeId];
            AddLink(fromNode, toNode, distance, linkData, directed, coordinates);
        }

        public void AddLink(int fromNodeId, int toNodeId, Distance distance, LinkData linkData, bool directed, IEnumerable<Point2D> coordinates = null)
        {
            var fromNode = NodesId[fromNodeId];
            var toNode = NodesId[toNodeId];
            AddLink(fromNode, toNode, distance, linkData, directed, coordinates);
        }


        /// <summary>
        /// Adds an arc to the graph and calculates distance between nodes coordinates
        /// </summary>
        /// <param name="fromNode">The origin node</param>
        /// <param name="toNode">The destination node</param>
        /// <param name="fetchedArcAttributes">The attributes to store for this arc</param>
        private void AddLink(Node fromNode, Node toNode, LinkData linkData, bool directed)
        {
            AddLink(fromNode, toNode, fromNode.Coordinate.GetDistanceTo(toNode.Coordinate), linkData, directed);
        }

        /// <summary>
        /// Adds an arc to the graph
        /// </summary>
        /// <param name="fromNode">The origin node</param>
        /// <param name="toNode">The destination node</param>
        /// <param name="distance">Provided distance</param>
        /// <param name="fetchedArcAttributes">The attributes to store for this arc</param>
        private void AddLink(Node fromNode, Node toNode, double distance, LinkData linkData, bool directed, IEnumerable<Point2D> coordinates = null)
        {
            if (Scenarios_Count != 0 && Scenarios_Count != 1)
                throw new InvalidOperationException("Cant add a link with a different amount of scenario values");
            else if (Scenarios_Count == 0)
                Scenarios_Count = 1;

            if (directed)
            {
                var newArc = new Arc()
                {
                    LinkData = linkData,
                    FromNode = fromNode,
                    ToNode = toNode,
                    FromNodeId = fromNode.Id,
                    ToNodeId = toNode.Id,
                    Distance = new Distance(new List<double> { distance},false),
                    Id = ++ArcsMaxId

                };
                if (coordinates != null)
                {
                    newArc.PointsData = Newtonsoft.Json.JsonConvert.SerializeObject(coordinates);
                }
                fromNode.OutgoingArcs.Add(newArc);
                toNode.IncomingArcs.Add(newArc);
                Arcs.Add(newArc);
            }
            else
            {
                var newEdge = new Edge()
                {
                    LinkData = linkData,
                    FromNode = fromNode,
                    ToNode = toNode,
                    FromNodeId = fromNode.Id,
                    ToNodeId = toNode.Id,
                    Distance = new Distance(new List<double> { distance }, false),
                    Id = ++EdgesMaxId
                };
                if (coordinates != null)
                {
                    newEdge.PointsData = Newtonsoft.Json.JsonConvert.SerializeObject(coordinates);
                }
                fromNode.Edges.Add(newEdge);
                //toNode.Edges.Add(newEdge.Reverse());
                toNode.Edges.Add(newEdge);
                Edges.Add(newEdge);
            }
        }


        private void AddLink(Node fromNode, Node toNode, Distance distance, LinkData linkData, bool directed, IEnumerable<Point2D> coordinates = null)
        {
            if (Scenarios_Count != 0 && Scenarios_Count != distance.Costs.Count)
                throw new InvalidOperationException("Cant add a link with a different amount of scenario values");
            else if (Scenarios_Count ==0)
                Scenarios_Count = distance.Costs.Count;
            if (directed)
            {
                var newArc = new Arc()
                {
                    LinkData = linkData,
                    FromNode = fromNode,
                    ToNode = toNode,
                    FromNodeId = fromNode.Id,
                    ToNodeId = toNode.Id,
                    Distance = distance,
                    Id = ++ArcsMaxId

                };
                if (coordinates != null)
                {
                    newArc.PointsData = Newtonsoft.Json.JsonConvert.SerializeObject(coordinates);
                }
                fromNode.OutgoingArcs.Add(newArc);
                toNode.IncomingArcs.Add(newArc);
                Arcs.Add(newArc);
            }
            else
            {
                var newEdge = new Edge()
                {
                    LinkData = linkData,
                    FromNode = fromNode,
                    ToNode = toNode,
                    FromNodeId = fromNode.Id,
                    ToNodeId = toNode.Id,
                    Distance = distance,
                    Id = ++EdgesMaxId
                };
                if (coordinates != null)
                {
                    newEdge.PointsData = Newtonsoft.Json.JsonConvert.SerializeObject(coordinates);
                }
                fromNode.Edges.Add(newEdge);
                //toNode.Edges.Add(newEdge.Reverse());
                toNode.Edges.Add(newEdge);
                Edges.Add(newEdge);


            }
        }



        /// <summary>
        /// Removes a link from the graph
        /// </summary>
        /// <param name="linkToDelete">The link to be deleted</param>
        public void DeleteLink(Link linkToDelete)
        {
            // Delete the link from its nodes and them from the graph
            if(linkToDelete.Directed)
            {
                linkToDelete.FromNode.OutgoingArcs.Remove((Arc)linkToDelete);
                linkToDelete.ToNode.IncomingArcs.Remove((Arc)linkToDelete);
                Arcs.Remove((Arc)linkToDelete);
            }
            else
            {
                linkToDelete.FromNode.Edges.Remove((Edge)linkToDelete);
                linkToDelete.ToNode.Edges.Remove((Edge)linkToDelete);
                Edges.Remove((Edge)linkToDelete);
            }            
        }

        /// <summary>
        /// Adds an arcData object to the ArcDataIndex
        /// </summary>
        /// <param name="linkData">The object to index</param>
        public void AddLinkData(LinkData linkData)
        {
            linkData.Id = ArcDatas.Count + 1;
            ArcDatas.Add(linkData);
        }
        #endregion

        #region Other Methods
        public void SplitNode (Node v1, Node v2, Node v3)
        {
            List<Link> v1_v2links = new List<Link>();
            List<Link> v2_v3links = new List<Link>();

            //Adding all arcs or edges v1-v2
            foreach (Link link in v1.OutgoingArcs)
            {
                if (link.ToNode == v2)
                    v1_v2links.Add(link);
            }
            foreach (Link link in v1.Edges)
            {
                if (link.ToNode == v2 || link.FromNode == v2)
                    v1_v2links.Add(link);
            }
            
            //Adding all arcs or edges v2-v3
            foreach (Link link in v2.OutgoingArcs)
            {
                if (link.ToNode == v3)
                    v2_v3links.Add(link);
            }
            foreach (Link link in v2.Edges)
            {
                if (link.ToNode == v3 || link.FromNode == v3)
                    v2_v3links.Add(link);
            }

            //Checking this links exists
            if (!(v1_v2links.Count > 0))
                return;
            if (!(v2_v3links.Count > 0))
                return;

            //Adding new split node
            int new_v2_id = AddNode(v2.Latitude, v2.Longitude, JsonConvert.DeserializeObject<Dictionary<string, string>>(v2.NodeData));

            //Adding links to new node
            foreach (Link link in v2.Links.Where(l => !v1_v2links.Contains(l)))
            {
                AddLink(link.FromNodeId == v2.Id? new_v2_id:link.FromNodeId, link.ToNodeId == v2.Id? new_v2_id : link.ToNodeId, link.LinkData, link.Directed);
            }

            //Deleting v2-v3 links from first node
            foreach (Link link in v2_v3links)
            {
                DeleteLink(link);
            }
            

            v2.HasPairNode = true;
            v2.PairNode = new_v2_id;

            NodesId[new_v2_id].HasPairNode = true;
            NodesId[new_v2_id].PairNode = v2.Id;

        }
        #endregion


        /// <summary>
        /// Add as prohibitions all edge-to-arc left turns 
        /// </summary>
        public void AutomaticProhibitions ()
        {
            for (int i = 0; i < Nodes.Count; i++)
            {
                Node v1 = Nodes[i];
                List<Edge> v1_v2_edges = new List<Edge>();
                foreach (Edge edge in v1.Edges)
                    v1_v2_edges.Add(edge);

                for (int j = 0; j < v1_v2_edges.Count; j++)
                {
                    Node v2 = v1_v2_edges[j].FromNode == v1? v1_v2_edges[j].ToNode : v1_v2_edges[j].FromNode;
                    List<Arc> v2_v3_arcs = new List<Arc>();
                    foreach (Arc arc in v2.OutgoingArcs)
                        v2_v3_arcs.Add(arc);
                    for (int h = 0; h < v2_v3_arcs.Count; h++)
                    {
                        Node v3 = v2_v3_arcs[h].ToNode;

                        double v1X = v2.Coordinate[1] - v1.Coordinate[1];
                        double v1Y = v2.Coordinate[0] - v1.Coordinate[0];
                        double v2X = v3.Coordinate[1] - v2.Coordinate[1];
                        double v2Y = v3.Coordinate[0] - v2.Coordinate[0];

                        if(v1X * v2Y > v1Y * v2X)
                        {
                            TurnProhibitions.Add((v1, v2, v3));
                        }

                    }
                }
            }
        }


        #endregion
        public object Clone()
        {
            var newGraph = new Graph();

            foreach (var node in Nodes)
            {
                var newNode = new Node()
                {
                    Latitude = node.Latitude,
                    Longitude = node.Longitude,
                };
                newGraph.AddNode(newNode, node.NodeAttributes);
            }

            foreach (var arc in Arcs)
            {
                newGraph.AddLink(arc.FromNodeId, arc.ToNodeId, new Distance(arc.Distance.Costs,arc.Distance.IsMultiScenario) ,arc.LinkData, true);
            }

            foreach (var edge in Edges)
            {
                newGraph.AddLink(edge.FromNodeId, edge.ToNodeId, new Distance(edge.Distance.Costs, edge.Distance.IsMultiScenario), edge.LinkData, false);
            }

            foreach(var tp in TurnProhibitions)
            {
                (Node, Node, Node) new_tp = (newGraph.NodesId[tp.Item1.Id], newGraph.NodesId[tp.Item2.Id], newGraph.NodesId[tp.Item3.Id]);
                newGraph.TurnProhibitions.Add(new_tp);
            }
            return newGraph;
        }
    }
}
