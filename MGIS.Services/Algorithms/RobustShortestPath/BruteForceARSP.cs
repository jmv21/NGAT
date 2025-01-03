using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGAT.Business.Contracts.Services.Algorithms;
using NGAT.Business.Domain.Core;
using NGAT.Geo.Geometries;
using NGAT.Services.Algorithms.RobustShortestPath;
namespace NGAT.Services.Algorithms
{
    public class BruteForceARSP : IShortestPathProblemAlgorithm
    {

        public string UniqueId => "Brute Force ARSP";

        public string Description => "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="args">Must be: object[3], args[0]->start point and args[1]-> end point, args[3] -> matrix with scenarios.</param>
        /// <returns></returns>
        public ShortestPathProblemOutput Run(Graph graph, Node sp, Node ep)
        {

            #region Making multi-scenario matrix

            double[][][] costs = new double[2][][];
            for (int i = 0; i < 2; i++)
            {
                costs[i] = new double[graph.Nodes.Count][];
                for (int j = 0; j < graph.Nodes.Count; j++)
                {
                    costs[i][j] = new double[graph.Nodes.Count];
                }
            }

            for (int i = 0; i < 2; i++)
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
                        Node v1 = graph.NodesId[j+1];
                        Node v2 = graph.NodesId[k+1];

                        Arc v1_v2_arc = v1.OutgoingArcs.FirstOrDefault(l => l.ToNode == v2);
                        Edge v1_v2_edge = v1.Edges.FirstOrDefault(l => l.ToNode == v2 || l.FromNode == v2);

                        if (v1_v2_arc != null)
                        {
                            costs[i][j][k] = v1_v2_arc.Distance.Costs[i];
                        }
                        else if (v1_v2_edge != null)
                        {
                            costs[i][j][k] = v1_v2_edge.Distance.Costs[i];

                        }
                        else
                        {
                            costs[i][j][k] = -1;
                        }
                    }
                }
            }
            #endregion

            return Run(sp, ep, costs, graph);
        }

        public ShortestPathProblemOutput Run(Node sp, Node ep, double[][][] costs, Graph graph)
        {
            List<(List<int>, List<double>)> paths = RobustShortestPath.Utils.CalculatePaths(costs, sp.Id - 1, ep.Id - 1);
            double best = double.MaxValue;
            int indexOf = 0;
            for (int i = 0; i < paths.Count; i++)
            {
                double worst = 0;
                for (int j = 0; j < paths[i].Item2.Count; j++)
                {
                    if (paths[i].Item2[j] > worst)
                        worst = paths[i].Item2[j];
                }
                if (worst < best)
                {
                    best = worst;
                    indexOf = i;
                }
            }
            List<int> nodesId = new List<int>();
            for (int i = 0; i < paths[indexOf].Item1.Count; i++)
            {
                nodesId.Add(paths[indexOf].Item1[i] + 1);
            }
            ShortestPathProblemOutput outPut = new ShortestPathProblemOutput(best, nodesId, paths[indexOf].Item1.Select(n => new PointLatLong(graph.NodesId[n+1].Latitude, graph.NodesId[n+1].Longitude)), sp, ep);
            return outPut;


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
