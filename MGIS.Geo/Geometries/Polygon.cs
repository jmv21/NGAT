using System.Collections.Generic;

namespace NGAT.Geo.Geometries
{
    public class Polygon : LineString
    {
        public Polygon(params Coordinate[] coordinates) : base(coordinates)
        {
            GeometryType = GeometryType.Polygon;
            if (coordinates[0] != coordinates[coordinates.Length - 1])
            {
                var temp = new List<Line2D>(Segments);
                temp.Add(new Line2D(coordinates[coordinates.Length - 1], coordinates[0]));
                Segments = temp.ToArray();
            }
        }

    }


}
