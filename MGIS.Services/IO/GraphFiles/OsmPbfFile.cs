using NGAT.Business.Contracts.IO;
using NGAT.Services.Containers;
using System;

namespace NGAT.Services.IO.MapFiles
{
    /// <summary>
    /// Represents an OpenStreetMap .pbf file.
    /// </summary>
    public class OsmPbfFile : GraphFile
    {
        public OsmPbfFile(Uri uri) : base("PBF", uri)
        {
            this.FileUri = uri;
            var container = new OsmPbfContainer();
            GraphBuilder = container.Resolve<IGraphBuilder>();
            GraphExporter = container.Resolve<IGraphExporter>();

        }

        public OsmPbfFile() : base("PBF", null)
        {
            var container = new OsmPbfContainer();
            var attrFetcher = container.Resolve<IAttributesFetcherCollection>();

            GraphBuilder = container.Resolve<IGraphBuilder>();
            GraphExporter = container.Resolve<IGraphExporter>();
        }

    }

}
