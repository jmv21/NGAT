using NGAT.Business.Contracts.Services.Layers;
using OsmSharp;
using System;

namespace NGAT.Services.Layers
{
    public class ShopPBFFilter : ILayerFilter
    {
        public Type ProviderType => typeof(OsmPBFLayerProvider);

        public string DefaultDataSource => @"..\Debug\Resources\IO\cuba-latest.osm.pbf";

        public string DefaultMarkerPath => @"..\Debug\Resources\BitMaps\supermarket_blue.png";



        public bool Match(object feature)
        {
            var tmp = (OsmGeo)feature;
            return tmp.Type == OsmGeoType.Node && tmp.Tags.ContainsKey("shop")
               && (tmp.Tags["shop"] == "general"  || tmp.Tags["shop"] == "department_store"); // || tmp.Tags["shop"] == "convenience"
        }

        public override string ToString()
        {
            return "Shop Filter";
        }
    }
}
