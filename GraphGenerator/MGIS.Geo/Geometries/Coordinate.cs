using NGAT.Geo.Contracts;
using Newtonsoft.Json;
using System;

namespace NGAT.Geo.Geometries
{
    public class Coordinate : IGeometry, IComparable<Coordinate>, IComparable
    {
        public Coordinate(params double[] ordinates)
        {
            GeometryType = GeometryType.Point;
            Ordinates = ordinates;
            if (ordinates.Length > 4)
                OrdinatesKind = OrdinatesKind.N;
            else
                OrdinatesKind = (OrdinatesKind)ordinates.Length;
        }

        public virtual double this[int index]
        {
            get
            {
                if (index < 0 || index >= Ordinates.Length)
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                return Ordinates[index];
            }
            set
            {
                if (index < 0 || index >= Ordinates.Length)
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                Ordinates[index] = value;
            }
        }

        [JsonIgnore]
        public virtual double[] Ordinates { get; set; }
        [JsonIgnore]
        public virtual OrdinatesKind OrdinatesKind { get; set; }
        [JsonIgnore]
        public virtual GeometryType GeometryType { get; set; }
        [JsonIgnore]
        public virtual double Area { get; }
        [JsonIgnore]
        public virtual double Length { get; }
        [JsonIgnore]
        public virtual double Volume { get; }
        [JsonIgnore]
        public Coordinate[] Coordinates => new Coordinate[] { this };
        [JsonIgnore]
        public IGeometry[] Geometries => Coordinates;
        [JsonIgnore]
        public ICoordinateSystem CoordinateSystem { get; set; }
        [JsonIgnore]
        public Envelope Envelope => new Envelope(this, this);

        public virtual double GetDistanceTo(Coordinate other)
        {
            return this.CoordinateSystem.GetDistanceTo(this, other);
        }

        public virtual bool InBoundary(IGeometry other)
        {
            var geom = Intersection(other, out Location location);
            return location == Location.Boundary;
        }

        public virtual IGeometry Intersection(IGeometry other, out Location location)
        {
            location = Location.Null;
            return null;
        }

        public override bool Equals(object obj)
        {
            var coordinate = obj as Coordinate;
            if (coordinate == null)
                return false;
            if (!SameDimensionAndCoordinateSystem(coordinate))
                return false;
            for (int i = 0; i < (int)OrdinatesKind; i++)
            {
                if (Ordinates[i] != coordinate.Ordinates[i])
                    return false;
            }
            return true;

        }

        public static bool operator ==(Coordinate first, Coordinate second)
        {
            if (object.ReferenceEquals(first, null) && object.ReferenceEquals(second, null))
                return true;
            else if (object.ReferenceEquals(first, null) || object.ReferenceEquals(second, null))
                return false;
            return first.Equals(second);
        }

        public static bool operator !=(Coordinate first, Coordinate second)
        {
            return !(first == second);
        }

        public static bool operator >(Coordinate first, Coordinate second)
        {

            if (!first.SameDimensionAndCoordinateSystem(second))
                return false;
            for (int i = 0; i < (int)first.OrdinatesKind; i++)
            {
                if (first[i] <= second[i])
                    return false;
            }
            return true;
        }

        public static bool operator <(Coordinate first, Coordinate second)
        {

            if (!first.SameDimensionAndCoordinateSystem(second))
                return false;
            for (int i = 0; i < (int)first.OrdinatesKind; i++)
            {
                if (first[i] >= second[i])
                    return false;
            }
            return true;
        }

        public static bool operator >=(Coordinate first, Coordinate second)
        {

            return first.Equals(second) || first > second;
        }

        public static bool operator <=(Coordinate first, Coordinate second)
        {

            return first.Equals(second) || first < second;
        }

        private bool SameDimensionAndCoordinateSystem(Coordinate other)
        {
            return OrdinatesKind == other.OrdinatesKind;
        }

        #region HashCode Taken From NetTopologySuite
        /// <summary>
        /// Gets a hashcode for this coordinate.
        /// </summary>
        /// <returns>A hashcode for this coordinate.</returns>
        public override int GetHashCode()
        {
            var result = 17;
            // ReSharper disable NonReadonlyFieldInGetHashCode
            for (int i = 0; i < (int)OrdinatesKind; i++)
            {
                result = 37 * result + GetHashCode(Ordinates[i]);
                result = 37 * result + GetHashCode(Ordinates[i]);
            }
            // ReSharper restore NonReadonlyFieldInGetHashCode
            return result;
        }

        /// <summary>
        /// Computes a hash code for a double value, using the algorithm from
        /// Joshua Bloch's book <i>Effective Java"</i>
        /// </summary>
        /// <param name="value">A hashcode for the double value</param>
        static int GetHashCode(double value)
        {
            return value.GetHashCode();

            // This was implemented as follows, but that's actually equivalent:
            /*
            var f = BitConverter.DoubleToInt64Bits(value);
            return (int)(f ^ (f >> 32));
            */
        }

        public int CompareTo(Coordinate other)
        {
            return other == null ? 1 : other == this ? 0 : other > this ? -1 : 1;
        }

        public int CompareTo(object obj)
        {
            return CompareTo(obj as Coordinate);
        }
        #endregion

    }
}
