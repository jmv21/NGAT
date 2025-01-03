using NGAT.Business.Domain.Core;
using System;

namespace NGAT.Business.Contracts.IO
{
    /// <summary>
    /// Represents a file that contains information about a graph and the input/output for running an algorithm over that graph.
    /// </summary>
    public abstract class GraphAlgorithmFile: File
    {
        /// <summary>
        /// Initializes a Map Result File.
        /// </summary>
        /// <param name="formatID">The upper case name or ID of this class of file. ex: "PBF", "GRF", ...</param>
        /// <param name="fileUri">The Uri of the file. It cant be null while importing file.</param>
        /// <param name="graph">The graph of the map.</param>
        public GraphAlgorithmFile(string formatID, Uri fileUri, Graph graph) : base(formatID, fileUri)
        {
            Graph = graph;
        }

        /// <summary>
        /// The graph inside the file.
        /// </summary>
        public Graph Graph { get; protected set; }

        /// <summary>
        /// Input to run an algorithm over the graph.
        /// </summary>
        public object Input { get; set; }

        /// <summary>
        /// The output produced by running an algorithm over the graph.
        /// </summary>
        public object Output { get; set; }

        /// <summary>
        /// Exports the GraphAlgorithmFile to a file.
        /// </summary>
        /// <param name="filePath"></param>
        public abstract void Export(string filePath);

        /// <summary>
        /// Imports the GraphAlgorithmFile from a file.
        /// </summary>
        public abstract void Import();

    }

}
