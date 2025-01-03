using NGAT.Business.Contracts.Services.Layers;
using OsmSharp;
using System;

namespace NGAT.Services.Layers
{
    class PBFTrafficSignalsFilter : ILayerFilter
    {
        public Type ProviderType => typeof(OsmPBFLayerProvider);

        public string DefaultDataSource => @"..\Debug\Resources\IO\cuba-latest.osm.pbf";

        public string DefaultMarkerPath => @"..\Debug\Resources\BitMaps\red_point.png";

        public bool Match(object feature)
        {
            var tmp = (OsmGeo)feature;
            return tmp.Type == OsmGeoType.Node && tmp.Tags.ContainsKey("highway") && tmp.Tags["highway"] == "traffic_signals";
        }

        public override string ToString()
        {
            return "Traffic Signals Filter";
        }
    }
}
