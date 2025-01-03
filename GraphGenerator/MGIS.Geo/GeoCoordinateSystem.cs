using NGAT.Geo.Contracts;
using NGAT.Geo.Geometries;

namespace NGAT.Geo
{
    public class GeoCoordinateSystem : ICoordinateSystem
    {
        const double RadiusOfEarth = 6371000;

        public GeoCoordinateSystem()
        {
            Kind = CoordinateSystemKind.GeoCoordinates;
        }

        public CoordinateSystemKind Kind { get; set; }

        public double GetDistanceTo(Coordinate from, Coordinate to)
        {
            var lat1Rad = (from[0] / 180d) * System.Math.PI;
            var lon1Rad = (from[1] / 180d) * System.Math.PI;
            var lat2Rad = (to[0] / 180d) * System.Math.PI;
            var lon2Rad = (to[1] / 180d) * System.Math.PI;

            var x = (lon2Rad - lon1Rad) * System.Math.Cos((lat1Rad + lat2Rad) / 2.0);
            var y = lat2Rad - lat1Rad;

            var m = System.Math.Sqrt(x * x + y * y) * RadiusOfEarth;

            return m;
        }
    }

}
