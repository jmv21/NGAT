namespace NGAT.Geo
{
    /// <summary>
    /// Represents the location of a point respect an IGeometry
    /// </summary>
    public enum Location
    {
        /// <summary>
        /// The coordinate is inside the geometry
        /// </summary>
        Interior = 0,
        /// <summary>
        /// The coordinate is in the frontier of the geometry
        /// </summary>
        Boundary = 1,
        /// <summary>
        /// The coordinate is outside of the geometry
        /// </summary>
        Exterior = 2,
        /// <summary>
        /// The geometry is both interior and exterior
        /// </summary>
        Partial = 3,
        /// <summary>
        /// Not initialized
        /// </summary>
        Null = -1
    }
}
