using NGAT.Business.Domain.Base;
using NGAT.Geo.Geometries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGAT.Business.Domain.Core
{
    /// <summary>
    /// Represents a graph node (vertex)
    /// </summary>
    public class Node : GraphDependantEntity, IComparable<Node>
    {
        public Node()
        {
            this.IncomingArcs = new List<Arc>();
            this.OutgoingArcs = new List<Arc>();
            this.Edges = new List<Edge>();

        }

        /// <summary>
        /// The coordinate for this node
        /// </summary>
        [JsonIgnore]
        public Point2D Coordinate
        {
            get
            {
                return new Point2D(this.Latitude, this.Longitude) { CoordinateSystem = Point2D.GeoCoordinateSystem };
            }
        }

        /// <summary>
        /// The Longitud for this node
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// The Latitude for this node
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// The data associated with this node, i.e: Name, Tags, etc, ideally, JSON-encoded
        /// </summary>
        [JsonIgnore]
        public string NodeData { get; set; }

        /// <summary>
        /// The Deserialized node Data
        /// </summary>
        public IDictionary<string, string> NodeAttributes
        {
            get
            {
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(this.NodeData);
            }
        }

        /// <summary>
        /// The outgoing arcs related to this node
        /// </summary>
        public virtual IList<Arc> OutgoingArcs { get; set; }

        /// <summary>
        /// The incoming arcs related to this node
        /// </summary>
        public virtual IList<Arc> IncomingArcs { get; set; }

        /// <summary>
        /// The edges related to this node
        /// </summary>
        public virtual IList<Edge> Edges { get; set; }

        /// <summary>
        /// All related links to this node
        /// </summary>
        public IEnumerable<Link> Links
        {
            get
            {
                foreach (var link in IncomingArcs)
                    yield return link;
                foreach (var link in OutgoingArcs)
                    yield return link;
                foreach (var link in Edges)
                    yield return link;
            }
        }

        /// <summary>
        /// In-degree
        /// </summary>
        public int InDegree => IncomingArcs.Count;

        /// <summary>
        /// Out-degree
        /// </summary>
        public int OutDegree => OutgoingArcs.Count;

        /// <summary>
        /// Degree
        /// </summary>
        public int Degree => Edges.Count;


        public override string ToString()
        {
            return "Node " + Id;
        }

        public int CompareTo(Node other)
        {
            return this.Id.CompareTo(other.Id);
        }



    }

}
