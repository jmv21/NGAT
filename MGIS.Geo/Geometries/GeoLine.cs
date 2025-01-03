using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGAT.Geo.Contracts;

namespace NGAT.Geo.Geometries
{
    /// <summary>
    /// Represents a line in a Geographic Coordinates System
    /// </summary>
    public class GeoLine : IGeometry
    {
        public GeoLine(Coordinate point ,double m)
        {
            Point = new PointLatLong(point[0], point[1]);
            M = m;
            N = Point.Longitude - M * Point.Latitude;
            CoordinateSystem = new GeoCoordinateSystem();
            Coordinates = new Coordinate[] { Point };
        }

        public GeoLine(Coordinate point1, Coordinate point2)
        {
            Point = new PointLatLong(point1[0], point1[1]);
            M = (point2[1] - point1[1]) / (point2[0] - point1[0]);
            N = Point.Longitude - M * Point.Latitude;
            CoordinateSystem = new GeoCoordinateSystem();
            Coordinates = new Coordinate[] { Point };
        }

        public PointLatLong Point { get; set; }

        public double M { get; protected set; }

        public double N { get; protected set; }

        public GeometryType GeometryType => GeometryType.Line;

        public Coordinate[] Coordinates { get; protected set; }

        public ICoordinateSystem CoordinateSystem { get; set; }

        public IGeometry[] Geometries => Coordinates;

        public double Area => 0d;

        public double Length => double.PositiveInfinity;

        public double Volume => 0d;

        public Envelope Envelope => throw new NotImplementedException();

        public IGeometry Intersection(IGeometry other, out Location location)
        {
            if (other.GeometryType == GeometryType.Point && other is Coordinate point)
            {
                double x = point[0];
                double y = point[1];
                if(y == M*x + N) // the point belongs to the line
                {
                    location = Location.Interior;
                    return new PointLatLong(x, y);
                }
                location = Location.Exterior;
                return null;
            }
            else if (other.GeometryType == GeometryType.Line && other is GeoLine line)
            {
                if(line.M == M) // the lines are paralel, there is no intersection or there is full intersection
                {
                    if(line.N == N) // Full intersection, they are the same line
                    {
                        location = Location.Interior;
                        return this;
                    }
                    location = Location.Exterior;
                    return null;
                }
                double latitude = (line.N - N) / (M - line.M);
                double longitude = M * latitude + N;
                location = Location.Partial;
                return new Coordinate(latitude, longitude);
            }
            else if (other.GeometryType == GeometryType.LineString && other is LineString lineString)
            {
                List<IGeometry> toCollection = new List<IGeometry>();
                foreach (var line2d in lineString.Segments)
                {
                    var intersection = Intersection(line2d, out Location loc);
                    if (loc == Location.Interior)
                    {
                        toCollection.Add(intersection);
                    }

                }
                if (toCollection.Count == 0)
                {
                    location = Location.Null;
                    return null;
                }
                else if (toCollection.Count == 1)
                {
                    location = Location.Interior;
                    return toCollection[0] as Coordinate;
                }
                location = Location.Partial;
                return new GeometryCollection(toCollection.ToArray());
            }
            else if (other.GeometryType == GeometryType.Polygon && other is Polygon poly)
            {
                throw new NotImplementedException();
            }
            throw new NotSupportedException();
        }

        public static bool operator <(Coordinate point, GeoLine line)
        {
            return point[1] < line.M * point[0] + line.N;
        }

        public static bool operator >(Coordinate point, GeoLine line)
        {
            return point[1] > line.M * point[0] + line.N;
        }

        public static bool operator <=(Coordinate point, GeoLine line)
        {
            return point[1] <= line.M * point[0] + line.N;
        }

        public static bool operator >=(Coordinate point, GeoLine line)
        {
            return point[1] >= line.M * point[0] + line.N;
        }
    }
}
