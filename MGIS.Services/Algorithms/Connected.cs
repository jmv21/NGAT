using NGAT.Business.Contracts.Services.Algorithms;
using NGAT.Business.Domain.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NGAT.Services.Algorithms
{
    public class CheckConectivity
    {

        public string UniqueId => "Connected";

        public string Description => "An algorithm for checking if a graph or subgraph is connected. It uses Floy-Warshall algorithm to do so.";

        /// <summary>
        /// Gets a list with the pairs of disconected nodes. Each node is returned with the number of disconnections at NodeData.
        /// </summary>
        /// <param name="graph">The graph to check connectivity.</param>
        /// <param name="args">Empty array in this case.</param>
        /// <returns>A list with the pairs of disconected nodes.</returns>
        public object Run(Graph graph, params object[] args)
        {
            var floydWarshall = new FloydWarshall();
            var distances = floydWarshall.Run(graph) as double[,];
            var N = graph.Nodes.Count;
            List<(Node, Node)> disconnected = new List<(Node, Node)>();

            for (int i = 0; i < N; i++)
            {
                graph.Nodes[i].NodeData = "0";
                for (int j = 0; j < N; j++)
                {
                    if (distances[i, j] == double.PositiveInfinity) // disconnection
                    {
                        //Store the number of unreachable nodes from Nodes[i]
                        graph.Nodes[i].NodeData = (int.Parse(graph.Nodes[i].NodeData) + 1).ToString();
                        disconnected.Add((graph.Nodes[i], graph.Nodes[j]));
                    }
                }
            }
            return disconnected;
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
