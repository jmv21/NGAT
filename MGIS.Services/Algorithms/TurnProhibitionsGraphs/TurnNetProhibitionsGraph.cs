using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGAT.Business.Contracts.Services.Algorithms;
using NGAT.Business.Domain.Core;

namespace NGAT.Services.Algorithms.TurnProhibitionsGraphs
{
    public class TurnNetProhibitionsGraph : ITurnProhibitionsAssociatedGraph
    {
        public Graph OriginalGraph { get ; set ; }
        public Graph DualGraph { get ; set ; }
        public int dualStartNodeId { get ; set ; }
        public int dualEndNodeId { get ; set ; }

        public Graph DualGraphBuilder(Graph OriginalGraph, int startNodeId, int endNodeId)
        {
            Graph adaptedOriginal = (Graph)OriginalGraph.Clone();

            List<double> fakeCosts = new List<double>();
            for (int i = 0; i < adaptedOriginal.Scenarios_Count; i++)
            {
                fakeCosts.Add(0);
            }

            while (adaptedOriginal.Edges.Count > 0)
            {
                adaptedOriginal.AddLink(adaptedOriginal.Edges[0].FromNodeId, adaptedOriginal.Edges[0].ToNodeId, (Distance)adaptedOriginal.Edges[0].Distance, adaptedOriginal.Edges[0].LinkData, true);
                adaptedOriginal.AddLink(adaptedOriginal.Edges[0].ToNodeId, adaptedOriginal.Edges[0].FromNodeId, (Distance)adaptedOriginal.Edges[0].Distance, adaptedOriginal.Edges[0].LinkData, true);
                adaptedOriginal.DeleteLink(adaptedOriginal.Edges[0]);
            }

            int ficStarNodeId = adaptedOriginal.AddNode(adaptedOriginal.NodesId[startNodeId].Latitude, adaptedOriginal.NodesId[startNodeId].Longitude, new Dictionary<string, string>());
            adaptedOriginal.AddLink(ficStarNodeId, startNodeId, new Distance(fakeCosts, adaptedOriginal.Scenarios_Count > 1), new LinkData(), true);

            int ficEndNodeId = adaptedOriginal.AddNode(adaptedOriginal.NodesId[endNodeId].Latitude, adaptedOriginal.NodesId[endNodeId].Longitude, new Dictionary<string, string>());
            adaptedOriginal.AddLink(endNodeId, ficEndNodeId, new Distance(fakeCosts, adaptedOriginal.Scenarios_Count > 1), new LinkData(), true);
            
            this.OriginalGraph = adaptedOriginal;
            
            Graph dual = new Graph();
            
            for (int i = 0; i < adaptedOriginal.Arcs.Count; i++)
            {
                dual.AddNode(adaptedOriginal.Arcs[i].ToNode.Latitude, adaptedOriginal.Arcs[i].ToNode.Longitude, new Dictionary<string, string>());
            }
            for (int i = 0; i < adaptedOriginal.Arcs.Count; i++)
            {
                foreach (Arc arc in adaptedOriginal.Arcs[i].ToNode.OutgoingArcs)
                {
                    //if (!adaptedOriginal.TurnProhibitions.Where(tp => (tp.Item1.Id == adaptedOriginal.Arcs[i].FromNodeId && tp.Item2.Id == adaptedOriginal.Arcs[i].ToNodeId && tp.Item3.Id == arc.ToNodeId)).Any())
                    dual.AddLink(dual.Nodes[i].Id, dual.Nodes[adaptedOriginal.Arcs.IndexOf(arc)].Id, (Distance)arc.Distance, arc.LinkData, true);
                }
            }
            dualStartNodeId = dual.NodesMaxId - 1;
            dualEndNodeId = dual.NodesMaxId;
            for (int i = 0; i < adaptedOriginal.TurnProhibitions.Count; i++)
            {
                Node v1 = adaptedOriginal.TurnProhibitions[i].Item1;
                Node v2 = adaptedOriginal.TurnProhibitions[i].Item2;
                Node v3 = adaptedOriginal.TurnProhibitions[i].Item3;
                //Arc arc_v1_v2 = v1.OutgoingArcs.First(l => l.ToNode == v2);
                //Arc arc_v2_v3 = v2.OutgoingArcs.First(l => l.ToNode == v3);
                //int indexV1_V2 = adaptedOriginal.Arcs.IndexOf(arc_v1_v2);
                //int indexV2_V3 = adaptedOriginal.Arcs.IndexOf(arc_v2_v3);
                Node v1_v2 = dual.Nodes[adaptedOriginal.Arcs.IndexOf(v1.OutgoingArcs.First(l => l.ToNode == v2))];
                Node v2_v3 = dual.Nodes[adaptedOriginal.Arcs.IndexOf(v2.OutgoingArcs.First(l => l.ToNode == v3))];
                dual.DeleteLink(v1_v2.OutgoingArcs.First(l => l.ToNode == v2_v3));
            }
            this.DualGraph = dual;
            return dual;
        }

        public List<int> EquivalentPath(List<int> DualPath)
        {
            List<int> equivalentPath = new List<int>();
            for (int i = 0; i < DualPath.Count-1; i++)
            {
                equivalentPath.Add(OriginalGraph.Arcs[DualPath[i]-1].ToNodeId);
            }
            return equivalentPath;
        }

        public override string ToString()
        {
            return "TurnNet Turn Prohibitions Algorithm";
        }
    }
}
