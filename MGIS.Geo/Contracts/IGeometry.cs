using NGAT.Geo.Geometries;

namespace NGAT.Geo.Contracts
{
    public interface IGeometry
    {
        GeometryType GeometryType { get; }

        Coordinate[] Coordinates { get; }

        ICoordinateSystem CoordinateSystem { get; set; }

        IGeometry[] Geometries { get; }

        double Area { get; }

        double Length { get; }

        double Volume { get; }

        IGeometry Intersection(IGeometry other, out Location location);

        Envelope Envelope { get; }


    }
}
