using NGAT.Business.Domain.Core;
using System.Threading.Tasks;

namespace NGAT.Business.Contracts.Services.Algorithms
{
    /// <summary>
    /// Represents an algorithm with input and output
    /// </summary>
    /// <typeparam name="TOutput">The type of this algorithm response (A wrapper for output)</typeparam>
    public interface IShortestPathProblemAlgorithm
    {
        /// <summary>
        /// Executes the algorithm.
        /// </summary>
        /// <param name="input">The input for this algorithm</param>
        /// <returns>The output of this algorithm.</returns>
        ShortestPathProblemOutput Run(Graph graph, Node startPoint, Node endPoint);

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
    }
}
