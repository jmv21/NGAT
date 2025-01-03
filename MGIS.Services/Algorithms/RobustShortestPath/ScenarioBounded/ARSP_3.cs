using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGAT.Business.Contracts.Services.Algorithms;
using NGAT.Business.Domain.Core;
using NGAT.Geo.Geometries;
using NGAT.Services.Algorithms.RobustShortestPath.ScenarioBounded;

namespace NGAT.Services.Algorithms.RobustShortestPath.ScenarioBounded
{
    public class ARSP_3 : IBoundedARSP
    {
        public string UniqueId => "ARSP_3";

        public string Description => "";

        public int ScenariosCount => 3;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="args">Must be: object[2], args[0]->start point and args[1]-> end point.</param>
        /// <returns></returns>
        public ShortestPathProblemOutput Run(Graph graph, Node sp, Node ep)
        {


            #region Making multi-scenario matrix

            int[][][] costs = new int[3][][];
            for (int i = 0; i < 3; i++)
            {
                costs[i] = new int[graph.Nodes.Count][];
                for (int j = 0; j < graph.Nodes.Count; j++)
                {
                    costs[i][j] = new int[graph.Nodes.Count];
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < graph.Nodes.Count; j++)
                {
                    for (int k = 0; k < graph.Nodes.Count; k++)
                    {
                        if (j == k)
                        {
                            costs[i][j][k] = 0;
                            continue;
                        }
                        Node v1 = graph.NodesId[j + 1];
                        Node v2 = graph.NodesId[k + 1];

                        Arc v1_v2_arc = v1.OutgoingArcs.FirstOrDefault(l => l.ToNode == v2);
                        Edge v1_v2_edge = v1.Edges.FirstOrDefault(l => l.ToNode == v2 || l.FromNode == v2);

                        if (v1_v2_arc != null)
                        {
                            costs[i][j][k] = (int)v1_v2_arc.Distance.Costs[i];
                        }
                        else if (v1_v2_edge != null)
                        {
                            costs[i][j][k] = (int)v1_v2_edge.Distance.Costs[i];

                        }
                        else
                        {
                            costs[i][j][k] = -1;
                        }
                    }
                }
            }
            #endregion

            return Run(costs, sp.Id - 1, ep.Id - 1, graph);
        }

        public ShortestPathProblemOutput Run(int[][][] costs, int startPointIndex, int endPointIndex, Graph graph)
        {

            #region Getting Pr

            int[] Pr = new int[costs.Length];
            for (int i = 0; i < costs.Length; i++)
            {
                List<int> I_A = new List<int>();
                for (int j = 0; j < costs[i].Length; j++)
                {
                    I_A.Add(0);
                }

                for (int j = 0; j < costs[i].Length; j++)
                    for (int k = 0; k < costs[i].Length; k++)
                    {
                        for (int l = 0; l < I_A.Count; l++)
                        {
                            if (costs[i][j][k] != -1 && costs[i][j][k] > I_A[l])
                            {
                                I_A.Insert(l, costs[i][j][k]);
                                I_A.RemoveAt(I_A.Count - 1);
                            }
                        }
                    }
                Pr[i] = I_A.Sum();
            }
            #endregion

            int PrMax = Pr.Max();

            for (int i = 0; i < costs.Length; i++)
            {
                for (int j = 0; j < costs[0].Length; j++)
                {
                    for (int k = 0; k < costs[i][j].Length; k++)
                    {
                        if (costs[i][j][k] == -1)
                            costs[i][j][k] = PrMax;
                    }
                }
            }
            // dp[t,v,pr_0,...,pr_S]
            int[,,,,] dp = new int[costs[0].Length - 1, costs[0].Length, Pr[0] + 1, Pr[1] + 1, Pr[2] + 1];


            for (int s1 = 0; s1 < Pr[0]; s1++)
            {
                for (int s2 = 0; s2 < Pr[1]; s2++)
                {
                    for (int s3 = 0; s3 < Pr[2]; s3++)
                    {
                        for (int v = 0; v < costs[0].Length; v++)
                        {
                            dp[0, v, s1, s2, s3] = Math.Min(Math.Max(s1 + costs[0][v][endPointIndex], Math.Max(s2 + costs[1][v][endPointIndex], s3 + costs[2][v][endPointIndex])), PrMax);
                        }
                    }
                }
            }


            for (int t = 1; t < costs[0].Length - 1; t++)
            {
                for (int v = 0; v < costs[0].Length; v++)
                {
                    for (int s1 = 0; s1 < Pr[0]; s1++)
                    {
                        for (int s2 = 0; s2 < Pr[1]; s2++)
                        {
                            for (int s3 = 0; s3 < Pr[2]; s3++)
                            {

                                dp[t, v, s1, s2, s3] = PrMax;
                                for (int v2 = 0; v2 < costs[0].Length; v2++)
                                {
                                    if (
                                        (s1 + costs[0][v][v2] < Pr[0] && s2 + costs[1][v][v2] < Pr[1] && s3 + costs[2][v][v2] < Pr[2])
                                        && dp[t - 1, v2, s1 + costs[0][v][v2], s2 + costs[1][v][v2], s3 + costs[2][v][v2]] < dp[t, v, s1, s2, s3]
                                        )
                                    {
                                        dp[t, v, s1, s2, s3] = dp[t - 1, v2, s1 + costs[0][v][v2], s2 + costs[1][v][v2], s3 + costs[2][v][v2]];
                                    }
                                }
                            }
                        }
                    }
                }
            }

            int distance = dp[costs[0].Length - 2, startPointIndex, 0, 0, 0];

            List<int> path = new List<int>();

            #region Building Path
            int nextpoint = startPointIndex;
            int t_r = costs[0].Length - 2;
            int s1_r = 0;
            int s2_r = 0;
            int s3_r = 0;
            path.Add(nextpoint);
            while ((nextpoint != endPointIndex) && (t_r > 0))
            {
                for (int v = 0; v < costs[0].Length; v++)
                {
                    if (s1_r + costs[0][nextpoint][v] < Pr[0] + 1 && s2_r + costs[1][nextpoint][v] < Pr[1] + 1 && s3_r + costs[2][nextpoint][v] < Pr[2] + 1 && dp[t_r, nextpoint, s1_r, s2_r, s3_r] == dp[t_r - 1, v, s1_r + costs[0][nextpoint][v], s2_r + costs[1][nextpoint][v], s3_r + costs[2][nextpoint][v]])
                    {
                        s1_r = s1_r + costs[0][nextpoint][v];
                        s2_r = s2_r + costs[1][nextpoint][v];
                        s3_r = s3_r + costs[2][nextpoint][v];
                        nextpoint = v;
                        t_r--;
                        break;
                    }
                }
                if (nextpoint != path.Last())
                    path.Add(nextpoint);
            }
            if (endPointIndex != path.Last())
                path.Add(endPointIndex);
            #endregion


            List<int> nodesId = new List<int>();
            for (int i = 0; i < path.Count; i++)
            {
                nodesId.Add(path[i] + 1);
            }

            ShortestPathProblemOutput output = new ShortestPathProblemOutput(distance, nodesId, nodesId.Select(n => new PointLatLong(graph.NodesId[n].Latitude, graph.NodesId[n].Longitude)), graph.NodesId[startPointIndex + 1], graph.NodesId[endPointIndex + 1]);
            return output;
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
