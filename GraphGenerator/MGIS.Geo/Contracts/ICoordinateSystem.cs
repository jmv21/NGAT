using NGAT.Geo.Geometries;

namespace NGAT.Geo.Contracts
{
    /// <summary>
    /// Represents a Coordinate System
    /// </summary>
    public interface ICoordinateSystem
    {
        /// <summary>
        /// The kind of this CoordinateSystem
        /// </summary>
        CoordinateSystemKind Kind { get; set; }

        /// <summary>
        /// Gets the distance between two coordinates
        /// </summary>
        /// <param name="from">The first coordinate</param>
        /// <param name="to">The second coordinate</param>
        /// <returns>The distance between two coordinates</returns>
        double GetDistanceTo(Coordinate from, Coordinate to);
    }

}
