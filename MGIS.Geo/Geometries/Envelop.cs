namespace NGAT.Geo.Geometries
{
    public class Envelope
    {
        public Envelope(Coordinate min, Coordinate max)
        {
            MinCoordinate = min;
            MaxCoordinate = max;
        }

        public Coordinate MinCoordinate { get; set; }

        public Coordinate MaxCoordinate { get; set; }

        public bool IntersectsWith(Envelope other)
        {
            return !(other.MinCoordinate > MaxCoordinate || other.MaxCoordinate < MinCoordinate);
        }

        public bool IntersectsWith(Coordinate coordinate)
        {
            return coordinate[0] >= MinCoordinate[0] && coordinate[1] >= MinCoordinate[1]
                && coordinate[0] <= MaxCoordinate[0] && coordinate[1] <= MaxCoordinate[1];
        }

        public bool Contains(Envelope other)
        {
            return other.MinCoordinate >= MinCoordinate && other.MaxCoordinate <= MaxCoordinate;
        }

    }


}
