using DotSpatial.Data;
using NGAT.Business.Contracts.Services.Layers;
using System;

namespace NGAT.Services.Layers
{
    public class CimexShapeFileFilter : ILayerFilter
    {
        public Type ProviderType => typeof(ShapeFileLayerProvider);

        public string DefaultDataSource => @"..\Debug\Resources\ShapeFiles\gis_osm_pois_free_1.shp";

        public string DefaultMarkerPath => @"..\Debug\Resources\BitMaps\supermarket.png";

        public bool Match(object feature)
        {
            var f = (Feature)feature;
            return f.DataRow.ItemArray[3].ToString().ToLower().Contains("cimex");
        }

        public override string ToString()
        {
            return "CIMEX";
        }
    }
}
