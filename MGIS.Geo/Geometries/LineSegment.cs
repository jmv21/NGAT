using NGAT.Geo.Contracts;

namespace NGAT.Geo.Geometries
{
    public class LineSegment : IGeometry
    {
        public const double E = 0.0000001;

        public Coordinate FromPoint { get; set; }

        public Coordinate ToPoint { get; set; }

        public GeometryType GeometryType => GeometryType.Line;

        public Coordinate[] Coordinates => new Coordinate[] { FromPoint, ToPoint };

        public IGeometry[] Geometries => Coordinates;

        public double Area => 0d;

        public double Length => FromPoint.GetDistanceTo(ToPoint);

        public double Volume => 0d;

        public ICoordinateSystem CoordinateSystem { get; set; }

        public virtual Envelope Envelope
        {
            get
            {
                if (FromPoint <= ToPoint)
                    return new Envelope(FromPoint, ToPoint);
                return new Envelope(ToPoint, FromPoint);
            }
        }

        public virtual IGeometry Intersection(IGeometry other, out Location location) { location = Location.Null; return null; }
    }


}
