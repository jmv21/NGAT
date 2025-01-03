using System;
using DotSpatial.Data;
using NGAT.Business.Contracts.Services.Layers;

namespace NGAT.Services.Layers
{
    public class HospitalShapeFileFilter : ILayerFilter
    {
        public Type ProviderType => typeof(ShapeFileLayerProvider);

        public string DefaultDataSource => @"..\Debug\Resources\ShapeFiles\gis_osm_pois_free_1.shp";

        public string DefaultMarkerPath => @"..\Debug\Resources\BitMaps\hospital.png";

        public bool Match(object feature)
        {
            var f = (Feature)feature;
            return (string)f.DataRow.ItemArray[2] == "hospital" || (string)f.DataRow.ItemArray[2] == "clinic";
        }

        public override string ToString()
        {
            return "Hospital";
        }
    }
}
