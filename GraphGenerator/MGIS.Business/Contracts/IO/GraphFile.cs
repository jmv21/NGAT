using NGAT.Business.Domain.Core;
using System;

namespace NGAT.Business.Contracts.IO
{
    /// <summary>
    /// Represents a file which contains the information of a graph.
    /// </summary>
    public abstract class GraphFile : File
    {

        public GraphFile(string formatID, Uri fileUri) : base(formatID, fileUri)
        {

        }

        /// <summary>
        /// The graph inside the file.
        /// </summary>
        public Graph Graph { get; set; }

        /// <summary>
        /// The graph builder for this type of file.
        /// </summary>
        public IGraphBuilder GraphBuilder { get; protected set; }

        /// <summary>
        /// The graph exporter for this type of file.
        /// </summary>
        public IGraphExporter GraphExporter { get; set; }

    }

}
