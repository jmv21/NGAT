namespace NGAT.Geo.Geometries
{
    public class PointLatLong : Coordinate
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public PointLatLong(double latitude, double longitude) : base(latitude, longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
            this.CoordinateSystem = new GeoCoordinateSystem();
        }

    }
}
