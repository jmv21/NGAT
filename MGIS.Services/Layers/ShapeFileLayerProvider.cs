using System;
using System.Collections.Generic;
using System.Linq;
using DotSpatial.Data;
using DotSpatial.Topology;
using NGAT.Geo.Geometries;
using NGAT.Business.Contracts.Services.Layers;
using NGAT.Business.Domain.Core;

namespace NGAT.Services.Layers
{
    /// <summary>
    /// Represents an ESRI ShapeFile layer provider for any type of geometries.
    /// </summary>
    public class ShapeFileLayerProvider : FileLayerProvider
    {
        public ShapeFileLayerProvider() : base("SHP")
        {
        }

        /// <summary>
        /// Gets a layer from a shape file.
        /// </summary>
        /// <param name="dataSource">The path of a shape file.</param>
        /// <param name="filter">A shape file filter</param>
        /// <param name="layerName">The name of the resulting layer</param>
        /// <param name="iconPath">The icon's path of the layer.</param>
        /// <returns></returns>
        public override Layer GetLayer(string dataSource, ILayerFilter filter, string layerName, string iconPath)
        {
            if (!GetType().IsSubclassOf(filter.ProviderType) && filter.ProviderType != this.GetType())
                throw new ArgumentException("The selected filter does not match the Shape File Layer Provider.");

            List<Geo.Contracts.IGeometry> geometries = new List<Geo.Contracts.IGeometry>();
            Shapefile shp = Shapefile.OpenFile(dataSource);
            foreach (var feature in shp.Features.Where(f => filter.Match(f)))
            {
                
                switch (feature.FeatureType)
                {
                    case FeatureType.Point:
                        Point2D point = new Point2D(feature.Coordinates[0].Y, feature.Coordinates[0].X);
                        geometries.Add(point);
                        break;
                    case FeatureType.Polygon:
                        List<Geo.Geometries.Coordinate> coordinates = new List<Geo.Geometries.Coordinate>();
                        foreach (var c in feature.Coordinates)
                            coordinates.Add(new Geo.Geometries.Coordinate(c.Y, c.X));
                        var polygon = new Geo.Geometries.Polygon(coordinates.ToArray());
                        geometries.Add(polygon);
                        break;
                    case FeatureType.MultiPoint:
                        List<Geo.Geometries.Coordinate> coords = new List<Geo.Geometries.Coordinate>();
                        foreach (var c in feature.Coordinates)
                            coords.Add(new Geo.Geometries.Coordinate(c.Y, c.X));
                        Geo.Geometries.LineString lineString = new Geo.Geometries.LineString(coords.ToArray());
                        break;
                    default:
                        break;
                }
            }
            return new Layer(layerName, iconPath, geometries);
        }

        public override string ToString()
        {
            return "Shape File Provider";
        }
    }
}
