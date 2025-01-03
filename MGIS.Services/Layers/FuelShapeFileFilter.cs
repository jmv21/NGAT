using System;
using DotSpatial.Data;
using NGAT.Business.Contracts.Services.Layers;

namespace NGAT.Services.Layers
{

    public class FuelShapeFileFilter : ILayerFilter
    {
        public Type ProviderType => typeof(ShapeFileLayerProvider);

        public string DefaultDataSource => @"..\Debug\Resources\ShapeFiles\gis_osm_traffic_free_1.shp";

        public string DefaultMarkerPath => @"..\Debug\Resources\BitMaps\fuel.png";

        public bool Match(object feature)
        {
            var f = (Feature)feature;
            return (string)f.DataRow.ItemArray[2] == "fuel";
        }

        public override string ToString()
        {
            return "Fuel";
        }
    }
}
