using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NGAT.Business.Contracts.Services.Algorithms;
using NGAT.Business.Domain.Core;

namespace NGAT.Services.Algorithms.TurnProhibitionsGraphs
{
    public class SplitProhibitionsGraph : ITurnProhibitionsAssociatedGraph
    {
        public Graph OriginalGraph { get; set; }
        public Graph DualGraph { get ; set ; }
        public List<int> ReplicateNodes {  get; private set; }
        public int dualStartNodeId { get ; set ; }
        public int dualEndNodeId { get; set ; }

        public Graph DualGraphBuilder(Graph OriginalGraph, int startNodeId, int endNodeId)
        {
            this.OriginalGraph = OriginalGraph;
            Graph dualGraph = OriginalGraph.Clone() as Graph;
            dualGraph.TurnProhibitions.Sort(new TurnProhibitionComparer());
            List<int> replicateNodes = new List<int>();
            List<Node> v3s = new List<Node>();
            for (int i = 0; i < dualGraph.TurnProhibitions.Count - 1; i++)
            {
                if (dualGraph.TurnProhibitions[i + 1].Item1 == dualGraph.TurnProhibitions[i].Item1 && dualGraph.TurnProhibitions[i + 1].Item2 == dualGraph.TurnProhibitions[i].Item2)
                    v3s.Add(dualGraph.TurnProhibitions[i].Item3);
                else
                {
                    v3s.Add(dualGraph.TurnProhibitions[i].Item3);
                    Node[] v3 = new Node[v3s.Count];
                    v3s.CopyTo(v3);
                    SplitNode(dualGraph, dualGraph.TurnProhibitions[i].Item1, dualGraph.TurnProhibitions[i].Item2, v3);
                    replicateNodes.Add(dualGraph.TurnProhibitions[i].Item2.Id);
                    v3s.Clear();
                    for (int j = i + 1; j < dualGraph.TurnProhibitions.Count; j++)
                    {
                        if (dualGraph.TurnProhibitions[j].Item2 == dualGraph.TurnProhibitions[i].Item1 && dualGraph.TurnProhibitions[j].Item3 == dualGraph.TurnProhibitions[i].Item2)
                            dualGraph.TurnProhibitions[j] = (dualGraph.TurnProhibitions[j].Item1, dualGraph.TurnProhibitions[j].Item2, dualGraph.NodesId[dualGraph.NodesMaxId]);
                        else if (dualGraph.TurnProhibitions[j].Item1 == dualGraph.TurnProhibitions[i].Item2 && dualGraph.TurnProhibitions[j].Item2 != dualGraph.TurnProhibitions[i].Item1 && dualGraph.TurnProhibitions[j].Item2 != dualGraph.TurnProhibitions[i].Item3)
                            dualGraph.TurnProhibitions.Add((dualGraph.NodesId[dualGraph.NodesMaxId], dualGraph.TurnProhibitions[j].Item2, dualGraph.TurnProhibitions[j].Item3));
                    }

                }
            }
            if (dualGraph.TurnProhibitions.Count > 0)
            {
                v3s.Add(dualGraph.TurnProhibitions[dualGraph.TurnProhibitions.Count - 1].Item3);
                Node[] lastV3 = new Node[v3s.Count];
                v3s.CopyTo(lastV3);
                SplitNode(dualGraph, dualGraph.TurnProhibitions[dualGraph.TurnProhibitions.Count - 1].Item1, dualGraph.TurnProhibitions[dualGraph.TurnProhibitions.Count - 1].Item2, lastV3);
                replicateNodes.Add(dualGraph.TurnProhibitions[dualGraph.TurnProhibitions.Count - 1].Item2.Id);
            }
            this.DualGraph = dualGraph;
            this.ReplicateNodes = replicateNodes;
            this.dualStartNodeId = startNodeId;
            this.dualEndNodeId = endNodeId;
            return dualGraph;
        }
        public List<int> EquivalentPath(List<int> DualPath)
        {
            int replicateIndex = OriginalGraph.Nodes.Count + 1;
            List<int> EquivalentPath = new List<int>();
            for (int i = 0; i < DualPath.Count; i++)
            {
                if (DualPath[i] >= replicateIndex)
                    EquivalentPath.Add(ReplicateNodes[DualPath[i] - replicateIndex]);
                else
                    EquivalentPath.Add(DualPath[i]);
            }
            return EquivalentPath;
        }
        public static void SplitNode(Graph graph, Node v1, Node v2, params Node[] v3s)
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
            foreach (Node v3 in v3s)
            {
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
            }
            //Checking this links exists
            if (!(v1_v2links.Count > 0))
                return;
            if (!(v2_v3links.Count > 0))
                return;

            //Adding new split node
            int new_v2_id = graph.AddNode(v2.Latitude, v2.Longitude, JsonConvert.DeserializeObject<Dictionary<string, string>>(v2.NodeData));

            //Adding links to new node
            graph.AddLink(v1.Id, new_v2_id, (Distance)v1_v2links.First().Distance, v1_v2links.First().LinkData, true);
            foreach (Link link in v2.Links.Where(l => !v2_v3links.Contains(l)))
            {
                if (link.Directed)
                {
                    if (link.FromNodeId == v1.Id)
                        continue;
                    else if (link.FromNodeId == v2.Id)
                        graph.AddLink(new_v2_id, link.ToNodeId, (Distance)link.Distance,link.LinkData, true);
                }
                else
                {
                    if (link.FromNodeId == v1.Id || link.ToNodeId == v1.Id)
                        continue;
                    else
                        graph.AddLink(new_v2_id, link.ToNodeId == v2.Id ? link.FromNodeId : link.ToNodeId, (Distance)link.Distance, link.LinkData, true);
                }
            }
            //Deleting v1-v2 links from first node
            foreach (Link link in v1_v2links)
            {
                graph.DeleteLink(link);
                if (!link.Directed)
                {
                    graph.AddLink(v2.Id, v1.Id, (Distance)link.Distance, link.LinkData, true);
                }
            }
        }

        public override string ToString()
        {
            return "Split Node Turn Prohibitions Algorithm";
        }
    }

    public class TurnProhibitionComparer : IComparer<(Node, Node, Node)>
    {
        public int Compare((Node, Node, Node) x, (Node, Node, Node) y)
        {
            if (x.Item1.Id < y.Item1.Id)
                return -1;
            else if (x.Item1.Id > y.Item1.Id)
                return 1;
            else
                return 0;
        }
    }
}
