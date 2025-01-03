using NGAT.Geo.Contracts;
using System;
using System.Collections.Generic;

namespace NGAT.Geo.Geometries
{
    /// <summary>
    /// Represents a line segment
    /// </summary>
    public class Line2D : LineSegment
    {
        public Line2D(Coordinate from, Coordinate to)
        {
            FromPoint = from;
            ToPoint = to;
        }

        public double M
        {
            get
            {
                return (FromPoint[1] - ToPoint[1]) / (FromPoint[0] - ToPoint[0]);
            }
        }
        public double N
        {
            get
            {
                return FromPoint[1] - FromPoint[0] * M;
            }
        }
        public double[] Vector
        {
            get
            {
                return new double[] { ToPoint[0] - FromPoint[0], ToPoint[1] - FromPoint[1] };
            }
        }

        public override Envelope Envelope
        {
            get
            {
                double minX = Math.Min(FromPoint[0], ToPoint[0]);
                double minY = Math.Min(FromPoint[1], ToPoint[1]);
                double maxX = Math.Max(FromPoint[0], ToPoint[0]);
                double maxY = Math.Max(FromPoint[1], ToPoint[1]);

                return new Envelope(new Coordinate(minX, minY), new Coordinate(maxX, maxY));
            }
        }

        public override IGeometry Intersection(IGeometry other, out Location location)
        {
            if (other.GeometryType == GeometryType.Point && other is Coordinate point)
            {
                if (Envelope.IntersectsWith(point))
                {

                    if ((double.IsInfinity(M) && point[0] == FromPoint[0])
                        || (Math.Abs(M) <= E && point[1] == FromPoint[1])
                        || (Math.Abs(M * point.Ordinates[0] + N - point.Ordinates[1]) <= E))
                    {
                        location = Location.Interior;
                        return point;
                    }
                }
                location = Location.Exterior;
                return null;
            }
            else if (other.GeometryType == GeometryType.Line && other is Line2D lineSegment)
            {
                if (Envelope.IntersectsWith(lineSegment.Envelope))
                {
                    try
                    {
                        double x1, y1 = double.NaN;
                        if ((double.IsInfinity(M) && double.IsInfinity(lineSegment.M)) || (Math.Abs(M - lineSegment.M) <= E))
                        {
                            //parallel lines FIX this
                            location = Location.Exterior;
                            return null;
                        }
                        else if (double.IsInfinity(M))
                        {
                            x1 = FromPoint[0];
                            y1 = lineSegment.M * x1 + lineSegment.N;

                        }
                        else if (double.IsInfinity(lineSegment.M))
                        {
                            x1 = lineSegment.FromPoint[0];
                            y1 = M * x1 + N;

                        }
                        else
                        {
                            x1 = (lineSegment.N - N) / (M - lineSegment.M);
                            //var x1 = ((M * FromPoint[0] - FromPoint[1] - M * lineSegment.FromPoint[0]) + lineSegment.FromPoint[1]) / (M - lineSegment.M);
                            y1 = M * x1 + N;//(M * (x1 - FromPoint[0]) + FromPoint[1]);
                        }
                        Point2D point2D = new Point2D(x1, y1);
                        if ((Intersection(point2D, out location) != null || lineSegment.Intersection(point2D, out location) != null)
                            && Intersection(point2D, out location) == lineSegment.Intersection(point2D, out location))
                        {
                            location = Location.Interior;
                            return point2D;
                        }
                        location = Location.Exterior;
                        return null;
                    }
                    catch
                    { }
                }
                location = Location.Exterior;
                return null;
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
                if (Envelope.IntersectsWith(poly.Envelope))
                {
                    var inter1 = FromPoint.Intersection(poly, out Location location1);
                    var inter2 = ToPoint.Intersection(poly, out Location location2);
                    if (inter1 != null && inter2 != null)
                    {

                        location = Location.Interior;
                        return this;

                    }
                }
                location = Location.Exterior;
                return null;
            }
            throw new NotSupportedException();
        }

    }

}
