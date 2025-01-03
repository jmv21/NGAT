using NGAT.Geo.Contracts;
using System;
using System.Collections.Generic;

namespace NGAT.Geo.Geometries
{
    public class Point2D : Coordinate
    {
        public static ICoordinateSystem GeoCoordinateSystem = new GeoCoordinateSystem();

        public double X => this[0];
        public double Y => this[1];

        public Point2D(double x, double y) : base(x, y)
        {

        }

        public override IGeometry Intersection(IGeometry other, out Location location)
        {
            switch (other.GeometryType)
            {
                case GeometryType.Point:
                    if (((Coordinate)this) == ((Coordinate)other))
                    {
                        location = Location.Interior;
                        return this;
                    }
                    location = Location.Exterior;
                    return null;
                case GeometryType.Polygon:
                    if (other is Polygon poly)
                    {
                        //Checking if this point is part of the polygon ring
                        foreach (var segment in poly.Segments)
                        {
                            if (segment.Intersection(this, out location) != null)
                            {
                                location = Location.Boundary;
                                return this;
                            }
                        }
                        //checking if its inside the polygon
                        var lineToTheRight = new Line2D(this, new Point2D(this[0], double.MaxValue - 10));
                        SortedSet<Point2D> hits = new SortedSet<Point2D>();

                        foreach (var segment in poly.Segments)
                        {
                            Point2D inter = null;
                            if ((inter = (Point2D)segment.Intersection(lineToTheRight, out location)) != null)
                                hits.Add(inter);
                        }
                        location = hits.Count % 2 == 0 ? Location.Exterior : Location.Interior;
                        return location == Location.Exterior ? null : this;
                    }
                    throw new NotSupportedException();
                case GeometryType.LineString:
                    var lineString = other as LineString;
                    if (lineString != null)
                    {
                        foreach (var geom in lineString.Segments)
                        {
                            if (geom.Intersection(this, out location).Equals(this))
                            {
                                location = Location.Interior;
                                return this;
                            }

                        }
                        location = Location.Exterior;
                        return null;
                    }
                    else throw new ArgumentNullException("other");

                case GeometryType.Line:
                    if (other is Line2D line2d)
                    {
                        return line2d.Intersection(this, out location);
                    }
                    throw new NotImplementedException();
                default:
                    throw new NotSupportedException();
            }
        }

        public override string ToString()
        {
            return $"({this[0].ToString()}, {this[1].ToString()})";
        }
    }

}
