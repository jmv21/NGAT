using NGAT.Business.Contracts.IO;
using NGAT.Business.Contracts.Services;
using NGAT.Services.IO;
using NGAT.Services.IO.Osm;

namespace NGAT.Services.Containers
{
    /// <summary>
    /// A container to resolve all dependencies of an Osm PBF file.
    /// </summary>
    public class OsmPbfContainer : Container
    {
        public OsmPbfContainer() : base()
        {
            Dictionary.Add(typeof(IGraphBuilder), typeof(OsmPbfGraphBuilder));
            Dictionary.Add(typeof(IAttributeFilterCollection), typeof(OsmRoadLinksFilterCollection));
            Dictionary.Add(typeof(IAttributesFetcherCollection), typeof(AttributesFetcherCollection));
            Dictionary.Add(typeof(IGraphExporter), typeof(GeoJSONGraphExporter));
        }

    }
}
