using Newtonsoft.Json;
using NGAT.Business.Domain.Core;
using NGAT.Geo.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGAT.Services.Algorithms
{
    public class AlgorithmUtils
    {
        public static Graph NonDirectedGraph(Graph graph)
        {
            Graph dualGraph = graph.Clone() as Graph;
            List<Arc> arcs = new List<Arc>();
            foreach (Arc arc in dualGraph.Arcs)
            {
                arcs.Add(arc);
                if (dualGraph.Edges.FirstOrDefault(edge => (edge.FromNode == arc.FromNode && edge.ToNode == arc.ToNode) || (edge.FromNode == arc.ToNode && edge.ToNode == arc.FromNode)) == null)
                {
                    dualGraph.AddLink(arc.FromNodeId, arc.ToNodeId, arc.Distance as Distance, arc.LinkData, false);
                }
            }
            foreach (Arc arc in arcs)
            {
                dualGraph.DeleteLink(arc);
            }
            return dualGraph;
        }
        public static (Node,Node) NearestStarEndPoints (Graph graph, PointLatLong startPoint, PointLatLong endPoint)
        {
            Node startNode = null;
            Node endNode = null;

            double startDist = double.PositiveInfinity;
            double endDist = double.PositiveInfinity;
            int bestStart = 0; // the index of the nearest node to startPoint
            int bestEnd = 0;
            for (int i = 0; i < graph.Nodes.Count; i++)
            {
                double dist = startPoint.GetDistanceTo(graph.Nodes[i].Coordinate);
                if (dist < startDist) // find the nearest node to startPoint
                {
                    startDist = dist;
                    bestStart = i;
                }
                dist = endPoint.GetDistanceTo(graph.Nodes[i].Coordinate);
                if (dist < endDist) // find the nearest node to endPoint
                {
                    endDist = dist;
                    bestEnd = i;
                }
            }
            startNode = graph.Nodes[bestStart];
            endNode = graph.Nodes[bestEnd];
            return (startNode,endNode);
        }
    }
}