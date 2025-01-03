using System;
using DotSpatial.Data;
using NGAT.Business.Contracts.Services.Layers;

namespace NGAT.Services.Layers
{
    public class HavanaMunicipalitiesFilter : ILayerFilter
    {
        public Type ProviderType => typeof(ShapeFileLayerProvider);

        public string DefaultDataSource => @"..\Debug\Resources\ShapeFiles\cub_admbnda_adm2_2019.shp";

        public string DefaultMarkerPath => @"..\Debug\Resources\BitMaps\supermarket.png";

        public bool Match(object feature)
        {
            var f = (Feature)feature;
            return ((string)f.DataRow.ItemArray[3]).ToLower().Contains("la habana")  && (string)f.DataRow.ItemArray[5] != "";
        }

        public override string ToString()
        {
            return "Havana Municipalities";
        }
    }
}
