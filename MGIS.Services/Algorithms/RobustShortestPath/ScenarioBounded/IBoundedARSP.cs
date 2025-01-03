using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGAT.Business.Domain.Core;

namespace NGAT.Services.Algorithms.RobustShortestPath.ScenarioBounded
{
    internal interface IBoundedARSP
    {
        /// <summary>
        /// Executes the algorithm.
        /// </summary>
        /// <param name="input">The input for this algorithm</param>
        /// <returns>The output of this algorithm.</returns>
        ShortestPathProblemOutput Run(Graph graph, Node sp, Node ep);

        /// <summary>
        /// Executes the algorithm asynchronously
        /// </summary>
        /// <param name="input">The input for this algorithm</param>
        /// <returns>The task that executes this algorithm</returns>
        Task<object> RunAsync(Graph graph, params object[] args);

        /// <summary>
        /// The unique id of this algorithm
        /// </summary>
        string UniqueId { get; }

        string Description { get; }
        int ScenariosCount { get; }
    }
}
