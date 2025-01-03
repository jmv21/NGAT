using NGAT.Business.Contracts.Services.Algorithms;
using NGAT.Business.Domain.Core;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NGAT.Services.Algorithms
{
    /// <summary>
    /// Floyd Warshalls algorithm implementation
    /// </summary>
    public class FloydWarshall
    {

        public bool IsRunning { get; set; }

        public string UniqueId => "FloydWarshall";

        public string Description => "Floyd-Warshall algorithm for computing the shortest paths costs from all to all nodes in a graph.";

        public object Run(Graph graph, params object[] args)
        {
            IsRunning = true;
            double[,] distances = null;
            try
            {
                distances = new double[graph.Nodes.Count, graph.Nodes.Count];
            }
            catch (Exception)
            {
                throw new Exception("Out of memory exception was thrown. Graph is to big for running Floyd-Warshall algorithm. You must continue without checking connectivity.");
            }
            


            #region Initialization
            for (int i = 0; i < graph.Nodes.Count; i++)
            {
                for (int j = 0; j < graph.Nodes.Count; j++)
                {
                    if (i == j)
                        distances[i, j] = 0;
                    else
                    {
                        var edge = graph.Nodes[i].Edges.FirstOrDefault(e => e.ToNodeId == graph.Nodes[j].Id || e.FromNodeId == graph.Nodes[j].Id);
                        var arc = graph.Nodes[i].OutgoingArcs.FirstOrDefault(a => a.ToNodeId == graph.Nodes[j].Id);

                        if (edge != null && arc != null)
                        {
                            distances[i, j] = Math.Min(edge.Distance.cost, arc.Distance.cost);
                        }
                        else if (edge != null)
                        {
                            distances[i, j] = edge.Distance.cost;
                        }
                        else if (arc != null)
                        {
                            distances[i, j] = arc.Distance.cost;
                        }
                        else
                            distances[i, j] = double.PositiveInfinity;
                    }
                }
            }
            #endregion

            var N = graph.Nodes.Count;
            for (int k = 0; k < N; k++)
                for (int j = 0; j < N; j++)
                    for (int i = 0; i < N; i++)
                    {
                        distances[i, j] = Math.Min(distances[i, j], distances[i, k] + distances[k, j]);
                    }


            IsRunning = false;
            return distances;
        }

        public Task<object> RunAsync(Graph graph, params object[] args)
        {
            return new Task<object>(() => Run(graph, args));
        }

        public override string ToString()
        {
            return UniqueId;
        }

    }

}
