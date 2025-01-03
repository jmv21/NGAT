using NGAT.Geo.Contracts;
using System.Collections.Generic;

namespace NGAT.Business.Domain.Core
{
    /// <summary>
    /// Represents a map vectorial layer.
    /// </summary>
    public class Layer
    {
        public string Name { get; protected set; }

        public string IconPath { get; protected set; }

        public IList<IGeometry> Geometries { get; protected set; }

        public Layer(string name, string iconPath, IList<IGeometry> geometries)
        {
            Name = name;
            IconPath = iconPath;
            Geometries = geometries;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
