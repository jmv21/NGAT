using NGAT.Business.Contracts.IO;
using NGAT.Business.Contracts.Services;
using NGAT.Services.IO;

namespace NGAT.Services.Containers
{
    /// <summary>
    /// A container to resolve all dependencies of an GRF file.
    /// </summary>
    public class GRFContainer : Container
    {
        public GRFContainer() : base()
        {
            Dictionary.Add(typeof(IGraphBuilder), typeof(GRFGraphBuilder));
            Dictionary.Add(typeof(IGraphExporter), typeof(GRFGraphExporter));
        }
    }
}
