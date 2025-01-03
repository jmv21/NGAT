using NGAT.Business.Contracts.IO;
using NGAT.Services.Containers;
using System;

namespace NGAT.Services.IO.MapFiles
{
    /// <summary>
    /// Represents a .grf file.
    /// </summary>
    public class GRFFile : GraphFile
    {
        public GRFFile() :base("GRF", null)
        {
            var container = new GRFContainer();
            GraphBuilder = container.Resolve<IGraphBuilder>();
            GraphExporter = container.Resolve<IGraphExporter>();
        }

        public GRFFile(Uri fileUri) : base("GRF", fileUri)
        {
            var container = new GRFContainer();
            GraphBuilder = container.Resolve<IGraphBuilder>();
            GraphExporter = container.Resolve<IGraphExporter>();
        }

    }

}
