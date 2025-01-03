namespace NGAT.Geo.Contracts
{
    public interface ICoordinate : IGeometry
    {
        double[] Ordinates { get; }

        OrdinatesKind OrdinatesKind { get; }

        double GetDistanceTo(ICoordinate other);

        double this[int index] { get; }

    }
}
