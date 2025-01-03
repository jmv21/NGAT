using NGAT.Geo.Contracts;
using System;
using System.Linq;

namespace NGAT.Geo.Geometries
{
    public class GeometryCollection : IGeometry
    {
        public GeometryCollection(params IGeometry[] geometries)
        {
            Geometries = geometries;
        }

        public GeometryType GeometryType => GeometryType.Collection;

        public Coordinate[] Coordinates { get; set; }

        public ICoordinateSystem CoordinateSystem { get; set; }

        public IGeometry[] Geometries { get; set; }

        public double Area => Geometries.Sum(g => g.Area);

        public double Length => Geometries.Sum(g => g.Length);

        public double Volume => Geometries.Sum(g => g.Volume);

        public Envelope Envelope => new Envelope(Geometries.Min(g => g.Envelope.MinCoordinate), Geometries.Max(g => g.Envelope.MaxCoordinate));

        public IGeometry Intersection(IGeometry other, out Location location)
        {
            throw new NotImplementedException();
        }

    }

}
