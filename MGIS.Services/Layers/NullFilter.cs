
using NGAT.Business.Contracts.Services.Layers;
using System;

namespace NGAT.Services.Layers
{
    public class NullFilter : ILayerFilter
    {
        /// <summary>
        /// Gets the type of object class, that means this filter can be used by all types of Layer Providers. 
        /// </summary>
        public Type ProviderType { get => typeof(object); }

        public string DefaultDataSource => string.Empty;

        public string DefaultMarkerPath => string.Empty;

        public bool Match(object feature)
        {
            return true;
        }
        public override string ToString()
        {
            return "Null Filter";
        }
    }
}
