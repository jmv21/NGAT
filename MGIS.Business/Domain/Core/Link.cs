using NGAT.Business.Domain.Base;
using NGAT.Geo.Geometries;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace NGAT.Business.Domain.Core
{
    public abstract class Link : Entity
    {
        /// <summary>
        /// The Id of the From Node
        /// </summary>
        public int FromNodeId { get; set; }

        /// <summary>
        /// The From Node
        /// </summary>
        public Node FromNode { get; set; }

        /// <summary>
        /// The Id of the To Node
        /// </summary>
        public int ToNodeId { get; set; }

        /// <summary>
        /// The To Node
        /// </summary>
        public Node ToNode { get; set; }

        /// <summary>
        /// The distance this link covers
        /// </summary>
        public ICost Distance { get; set; }

        /// <summary>
        /// The id of the data for this link
        /// </summary>
        public int LinkDataId { get; set; }

        /// <summary>
        /// The data for this arc
        /// </summary>
        public LinkData LinkData { get; set; }

        /// <summary>
        /// The sequence of points that compose this link
        /// </summary>
        public string PointsData { get; set; }

        public IEnumerable<Point2D> Points
        {
            get
            {
                if (PointsData != null)
                    return JsonConvert.DeserializeObject<Point2D[]>(PointsData);
                return new Point2D[0];
            }
        }

        /// <summary>
        /// A value indicating wether this link is directed (an arc) or not (an edge)
        /// </summary>
        public abstract bool Directed { get; }

    }

    /// <summary>
    /// Represents an arc
    /// </summary>
    public class Arc : Link
    {
        public override bool Directed => true;

        public override string ToString()
        {
            return $"[{FromNodeId}]-->[{ToNodeId}]";
        }
    }

    /// <summary>
    /// Represents an edge
    /// </summary>
    public class Edge : Link
    {
        public override bool Directed => false;

        /// <summary>
        /// Returns a copy of the edge, but swaping FromNode by ToNode.
        /// </summary>
        /// <returns></returns>
        public Edge Reverse()
        {
            Edge reverse = new Edge()
            {
                Id = this.Id,
                // Swap th direction
                    FromNode = this.ToNode,
                    ToNode = this.FromNode,
                    FromNodeId = this.ToNodeId,
                    ToNodeId = this.FromNodeId,
                ////////////////////////
                Distance = this.Distance,
                LinkData = this.LinkData,
                LinkDataId = this.LinkDataId,
                PointsData = this.PointsData
            };

            return reverse;
        }

        public override string ToString()
        {
            return $"{{{FromNodeId}]---[{ToNodeId}}}";
        }
    }

}
