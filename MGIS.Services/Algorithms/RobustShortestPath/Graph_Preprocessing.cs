using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGAT.Business.Domain.Core;
using NGAT.Geo.Geometries;

namespace NGAT.Services.Algorithms.RobustShortestPath
{
    public class Graph_Preprocessing
    {
        int scenarios_count;
        public Graph_Preprocessing(int scenarios_count)
        {
            this.scenarios_count = scenarios_count;
        }

        public Graph RDSP_Preprocessing(Graph graph, Node sp, Node ep )
        {
            Dijkstra algorithm = new Dijkstra();
            double RC_min =double.MaxValue;
            List<List<int>> paths = new List<List<int>>();
            List<double> LB = new List<double>();
            List<double> RC = new List<double>();
            List<(Link, bool)> robust_persistent_links = new List<(Link, bool)>();
            for (int i = 0; i < scenarios_count; i++)
            {
                Graph scenario = graph.Clone() as Graph;
                foreach (Link link in scenario.Links)
                {
                    link.Distance.cost = link.Distance.Costs[i];
                }
                dynamic result = algorithm.Run(graph, sp, ep);
                paths.Add(result.nodesId);
                LB.Add(result.Distance);
            }

            RC_min = double.MaxValue;
            for (int i = 0; i < paths.Count; i++)
            {
                List<double> costs = new List<double>();
                for (int j = 0; j < scenarios_count; j++)
                {
                    double cost = 0;
                    for (int k = 0; k < paths[i].Count - 1; j++)
                    {
                        Node v1 = graph.NodesId[paths[i][k]];
                        Node v2 = graph.NodesId[paths[i][k+1]];
                        Link link = v1.Links.FirstOrDefault(l => (l.ToNode == v2) || (!l.Directed && l.FromNode == v2));
                        cost = cost + link.Distance.Costs[j];
                    }
                    costs.Add(cost);
                }
                if (RC_min>costs.Max())
                {
                    RC_min=costs.Max();
                }
                RC.Add(costs.Max());
            }

            List<(Link,int,bool)> links = new List<(Link,int,bool)>();

            foreach (List<int> path in paths.Where(p => RC[paths.IndexOf(p)] == RC_min))
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Node v1 = graph.NodesId[path[i]];
                    Node v2 = graph.NodesId[path[i]];
                    Link link = v1.Links.FirstOrDefault(l => (l.ToNode == v2) || (!l.Directed && l.FromNode == v2));
                    bool inverted = link.ToNode != v2;
                    links.Add((link,paths.IndexOf(path),inverted));
                }
            }

            while (links.Count > 0)
            {
                (Link,int,bool) link = links.First();
                Graph scenario = graph.Clone() as Graph;
                int v1_index = link.Item1.FromNodeId;
                int v2_index = link.Item1.ToNodeId;
                Node v1 = scenario.NodesId[v1_index];
                Link link_to_delete = v1.Links.FirstOrDefault(l => l.ToNodeId == v2_index);
                if (link_to_delete is Edge)
                {
                    if (link.Item3)
                    {
                        scenario.AddLink(v1_index, v2_index, link_to_delete.LinkData, true);
                    }
                    else
                    {
                        scenario.AddLink(v1_index, v2_index, link_to_delete.LinkData, true);
                    }
                }
                else
                {
                    scenario.DeleteLink(link_to_delete);
                }
                foreach (Link l in scenario.Links)
                {
                    l.Distance.cost = l.Distance.Costs[link.Item2];
                }
                dynamic result = algorithm.Run(scenario, sp, ep);
                if (result.Distance != double.PositiveInfinity)
                {
                    double RDA = result.Distance - LB[link.Item2];
                    if (RDA > RC_min)
                        robust_persistent_links.Add((link.Item1, link.Item3));
                }
                links.RemoveAt(0);
            }

            #region Building new graph

            Graph preprocesed = graph.Clone() as Graph;
            foreach (var item in robust_persistent_links)
            {
                int v1_index = item.Item1.FromNodeId;
                int v2_index = item.Item1.ToNodeId;
                Node v1;
                Node v2;
                Link robust_persistent_link;
                if (item.Item1 is Edge)
                {
                    if (item.Item2)
                    {
                        v1 = preprocesed.NodesId[v2_index];
                        v2 = preprocesed.NodesId[v1_index];
                    }
                    else
                    {
                        v1 = preprocesed.NodesId[v1_index];
                        v2 = preprocesed.NodesId[v2_index];
                    }
                    robust_persistent_link = v1.Edges.FirstOrDefault(l => (l.ToNode == v2 || l.FromNode == v2));
                }
                else
                {
                    v1 = preprocesed.NodesId[v1_index];
                    v2 = preprocesed.NodesId[v2_index];
                    robust_persistent_link = v1.OutgoingArcs.FirstOrDefault(l => l.ToNode == v2);
                }

                if (robust_persistent_link != null)
                {
                    foreach (Link l in v1.OutgoingArcs.Concat(v2.IncomingArcs))
                    {
                        if (l != robust_persistent_link)
                        {
                            preprocesed.DeleteLink(l);
                        }
                    }
                    foreach (Link l in v1.Edges)
                    {
                        if (l != robust_persistent_link)
                        {
                            preprocesed.DeleteLink(l);
                            Node v3 = l.ToNode == v1 ? l.FromNode : l.ToNode;
                            preprocesed.AddLink(v3.Id, v1.Id, l.LinkData, true);
                        }
                    }
                    foreach (Link l in v2.Edges)
                    {
                        if (l != robust_persistent_link)
                        {
                            preprocesed.DeleteLink(l);
                            Node v3 = l.ToNode == v2 ? l.FromNode : l.ToNode;
                            preprocesed.AddLink(v2.Id, v3.Id, l.LinkData, true);
                        }
                    }
                }
            }

            #endregion

            return preprocesed;
        }
    }
}
