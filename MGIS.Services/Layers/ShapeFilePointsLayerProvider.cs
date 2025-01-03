using DotSpatial.Data;
using NGAT.Geo.Geometries;
using NGAT.Business.Contracts.Services.Layers;
using NGAT.Business.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NGAT.Services.Layers
{
    /// <summary>
    /// Represents an OpenStreetMap ShapeFile layer provider that maps geometries to points.
    /// </summary>
    public class ShapeFilePointsLayerProvider: ShapeFileLayerProvider
    {
        public override Layer GetLayer(string dataSource, ILayerFilter filter, string layerName, string iconPath)
        {
            if (!GetType().IsSubclassOf(filter.ProviderType) &&  filter.ProviderType != this.GetType())
                throw new ArgumentException("The selected filter does not match the Shape File Layer Provider.");

            List<Geo.Contracts.IGeometry> geometries = new List<Geo.Contracts.IGeometry>();
            Shapefile shp = Shapefile.OpenFile(dataSource);
            foreach (var feature in shp.Features.Where(f => filter.Match(f)))
            {
                Point2D point = new Point2D(feature.Centroid().BasicGeometry.Coordinates[0].Y, feature.Centroid().BasicGeometry.Coordinates[0].X);
                geometries.Add(point);
            }
            return new Layer(layerName, iconPath, geometries);
        }

        public override string ToString()
        {
            return "Shape File Points Provider";
        }
    }
}