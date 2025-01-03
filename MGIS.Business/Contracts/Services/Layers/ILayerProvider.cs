using NGAT.Business.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGAT.Business.Contracts.Services.Layers
{
    /// <summary>
    /// Represents a data source to create layers.
    /// </summary>
    public interface ILayerProvider
    {
        /// <summary>
        /// Gets a Layer object from dataSource whose geometries match filter.
        /// </summary>
        /// <param name="dataSource">A string that represents the data source of this provider.</param>
        /// <param name="filter">A filter to extract features from this Layer Provider</param>
        /// <param name="layerName">The name of the resulting layer.</param>
        /// <param name="iconPath">The icon's path of this layer.</param>
        /// <returns></returns>
        Layer GetLayer(string dataSource, ILayerFilter filter, string layerName, string iconPath);
    }
}
