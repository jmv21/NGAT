using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGAT.Business.Contracts.Services.Layers
{
    /// <summary>
    /// Represents a filter to extract features from a Layer Provider.
    /// </summary>
    public interface ILayerFilter
    {
        /// <summary>
        /// The type of the layer provider.
        /// </summary>
        Type ProviderType { get; }

        /// <summary>
        /// The default data source to use the filter.
        /// </summary>
        string DefaultDataSource { get; }

        /// <summary>
        /// The default icon path to draw the filtered points.
        /// </summary>
        string DefaultMarkerPath { get; }

        /// <summary>
        /// Matches feature with a condition.
        /// </summary>
        /// <param name="feature">A data feature from the provider.</param>
        /// <returns>True if feature matches the condition of the filter.</returns>
        bool Match(object feature);
    }
}
