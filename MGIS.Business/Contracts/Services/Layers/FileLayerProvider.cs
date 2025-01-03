using NGAT.Business.Contracts.IO;
using NGAT.Business.Domain.Core;

namespace NGAT.Business.Contracts.Services.Layers
{
    /// <summary>
    /// A file layer provider.
    /// </summary>
    public abstract class FileLayerProvider : File, ILayerProvider
    {
        /// <summary>
        /// Abstract class constructor.
        /// </summary>
        /// <param name="formatID">The upper case name or ID of this type of file.</param>
        public FileLayerProvider(string formatID) : base(formatID, null)
        {
        }

        /// <summary>
        /// Gets a Layer object from dataSource whose geometries match filter.
        /// </summary>
        /// <param name="dataSource">A string that represents the data source of this provider.</param>
        /// <param name="filter">A filter to extract features from this Layer Provider</param>
        /// <param name="layerName">The name of the resulting layer.</param>
        /// <param name="iconPath">The icon's path of this layer.</param>
        /// <returns></returns>
        public abstract Layer GetLayer(string dataSource, ILayerFilter filter, string layerName, string iconPath);
    }
}
