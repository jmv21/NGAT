using NGAT.Geo.Contracts;
using System;

namespace NGAT.Geo.Geometries
{
    public class Circle : IGeometry
    {
        public GeometryType GeometryType => GeometryType.Circle;

        public Coordinate[] Coordinates => throw new NotImplementedException();

        public ICoordinateSystem CoordinateSystem { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IGeometry[] Geometries => throw new NotImplementedException();

        public double Area => throw new NotImplementedException();

        public double Length => throw new NotImplementedException();

        public double Volume => throw new NotImplementedException();

        public Envelope Envelope => throw new NotImplementedException();

        public IGeometry Intersection(IGeometry other, out Location location)
        {
            throw new NotImplementedException();
        }
    }
}
