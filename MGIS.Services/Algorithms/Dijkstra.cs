using NGAT.Business.Contracts.Services.Algorithms;
using NGAT.Business.Domain.Core;
using NGAT.Geo.Geometries;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C5;

namespace NGAT.Services.Algorithms
{
    public class Dijkstra : IShortestPathProblemAlgorithm
    {
        public string UniqueId => "Dijkstra";

        public string Description => "Computes the best road between the two nearest nodes from the start and end points.";

        /// <summary>
        /// Computes the best road between the two nearest nodes from the start and end points.
        /// </summary>
        /// <param name="graph">The graph to run the algorithm.</param>
        /// <param name="args">Must be: PointLatLong[2], args[0]->start point and args[1]-> end point.</param>
        /// <returns>A list of an Anonymous Type which has two properties: double Latitude, double Longitude.</returns>
        public ShortestPathProblemOutput Run(Graph graph, Node sp, Node ep)
        {
            
            //PointLatLong startPoint = (PointLatLong)args[0];
            
            //PointLatLong endPoint = (PointLatLong)args[1];
            Node startNode = sp;
            Node endNode = ep;
            double[] distances = new double[graph.Nodes.Count];
            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = double.PositiveInfinity;
            }
            distances[graph.Nodes.IndexOf(startNode)] = 0;
            //double startDist = double.PositiveInfinity;
            //double endDist = double.PositiveInfinity;
            //int best = 0; // the index of the nearest node to startPoint
            //for (int i = 0; i < distances.Length; i++)
            //{
            //    double dist = startPoint.GetDistanceTo(graph.Nodes[i].Coordinate);
            //    if (dist < startDist) // find the nearest node to startPoint
            //    {
            //        startDist = dist;
            //        //distances[i] = dist;
            //        //startNode = graph.Nodes[i];
            //        best = i;
            //    }
            //    distances[i] = double.PositiveInfinity;
            //    dist = endPoint.GetDistanceTo(graph.Nodes[i].Coordinate);
            //    if (dist < endDist) // find the nearest node to endPoint
            //    {
            //        endDist = dist;
            //        endNode = graph.Nodes[i];
            //    }
            //}
            //startNode = graph.Nodes[best]; // update startNode
            //distances[best] = 0;

            Node[] parent = new Node[graph.Nodes.Count];
            bool[] visited = new bool[graph.Nodes.Count];


            IntervalHeap<(double,Node)> Q = new C5.IntervalHeap<(double,Node)>();
            Q.Add((0, startNode));
            
            while (Q.Count > 0)
            {
                var kv = Q.DeleteMin();
                var d = kv.Item1;
                var u = kv.Item2;
         
                int uIndex = graph.Nodes.IndexOf(u);
                visited[uIndex] = true;

                var uAdjacentNodesVisited = new List<int>(); // for multigraphs
                
                foreach (Node v in u.OutgoingArcs.Select(a => a.ToNode).Concat(u.Edges.Select(e => e.FromNode == u ? e.ToNode : e.FromNode)))
                {
                    int vIndex = graph.Nodes.IndexOf(v);
                    if (!visited[vIndex] && !uAdjacentNodesVisited.Contains(v.Id)
                        && (distances[vIndex] > distances[uIndex] + u.Links.First(l => (l.ToNode == v) || (!l.Directed && l.FromNode == v)).Distance.cost))
                    {
                        
                        uAdjacentNodesVisited.Add(v.Id);
                        distances[vIndex] = distances[uIndex] + u.Links.First(l => (l.ToNode == v) || (!l.Directed && l.FromNode == v)).Distance.cost;
                        parent[vIndex] = u;
                        Q.Add((distances[vIndex], v));
                    }
                }
            }

            List<Node> path = new List<Node>();
            List<int> nodesId = new List<int>();
            double distance = distances[graph.Nodes.IndexOf(endNode)];
            #region buildingPath

            var node = endNode;
            int nodeIndex = graph.Nodes.IndexOf(node);
            while (node != startNode)
            {
                path.Add(node);
                nodesId.Add(node.Id);
                if (parent[nodeIndex] == null)
                    return new ShortestPathProblemOutput(double.PositiveInfinity, nodesId, path.Select(n => new PointLatLong(n.Latitude, n.Longitude)), startNode, endNode);
                node = parent[nodeIndex];
                nodeIndex = graph.Nodes.IndexOf(node);
            }
            path.Add(startNode);
            path.Reverse();
            nodesId.Add(startNode.Id);
            nodesId.Reverse();
            // return a list of Anonymous Type which has two properties: double latitude, double longitude and the distance
            ShortestPathProblemOutput outPut = new ShortestPathProblemOutput(distance, nodesId, path.Select(n => new PointLatLong(n.Latitude, n.Longitude)), startNode, endNode);
            return outPut;
        

            #endregion
        }

        public Task<object> RunAsync(Graph graph, params object[] args)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return UniqueId;
        }

    }
}
