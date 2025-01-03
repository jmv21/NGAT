using DotSpatial.Data;
using NGAT.Business.Contracts.Services.Layers;
using System;

namespace NGAT.Services.Layers
{
    public class CupetShapeFileFilter : ILayerFilter
    {
        public Type ProviderType => typeof(ShapeFileLayerProvider);

        public string DefaultDataSource => @"..\Debug\Resources\ShapeFiles\gis_osm_traffic_free_1.shp";

        public string DefaultMarkerPath => @"..\Debug\Resources\BitMaps\fuel.png";

        public bool Match(object feature)
        {
            var f = (Feature)feature;
            return f.DataRow.ItemArray[3].ToString().ToLower().Contains("cupet");
        }

        public override string ToString()
        {
            return "CUPET";
        }
    }
}
