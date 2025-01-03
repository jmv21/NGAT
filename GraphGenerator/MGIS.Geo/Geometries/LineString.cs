using NGAT.Geo.Contracts;
using System;
using System.Linq;

namespace NGAT.Geo.Geometries
{
    public class LineString : IGeometry
    {
        public LineString(params Coordinate[] coordinates)
        {
            GeometryType = GeometryType.LineString;
            Coordinates = coordinates;
            Segments = new Line2D[Coordinates.Length - 1];
            for (int i = 0; i < Coordinates.Length - 1; i++)
            {
                Segments[i] = new Line2D(Coordinates[i], Coordinates[i + 1]);
            }
            FirstPoint = coordinates[0];
            LastPoint = coordinates[coordinates.Length - 1];
        }

        public Coordinate FirstPoint { get; set; }

        public Coordinate LastPoint { get; set; }

        public Line2D[] Segments { get; set; }

        public GeometryType GeometryType { get; set; }

        public Coordinate[] Coordinates { get; set; }

        public IGeometry[] Geometries => Segments;

        public double Area => 0d;

        public double Length => Segments.Sum(s => s.Length);

        public double Volume => 0d;

        public ICoordinateSystem CoordinateSystem { get; set; }

        public Envelope Envelope
        {
            get
            {
                double minX = Coordinates.Min(c => c[0]);//Math.Min(FromPoint[0], ToPoint[0]);
                double minY = Coordinates.Min(c => c[1]);
                double maxX = Coordinates.Max(c => c[0]);
                double maxY = Coordinates.Max(c => c[1]);
                return new Envelope(new Coordinate(minX, minY), new Coordinate(maxX, maxY));
            }
        }

        public IGeometry Intersection(IGeometry other, out Location location)
        {
            throw new NotImplementedException();
        }

    }

}
