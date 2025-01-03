using NGAT.Business.Domain.Core;
using NGAT.Geo;
using NGAT.Geo.Contracts;
using NGAT.Geo.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NGAT.Business
{

    public static class GeoExtensions
    {
        public static Point2D ToPoint2D(this Node node, ICoordinateSystem coordinateSystem)
        {
            if (node == null)
                throw new NullReferenceException();
            return new Point2D(node.Latitude, node.Longitude) { CoordinateSystem = coordinateSystem };
        }

        public static Line2D ToLine2D(this Link link, ICoordinateSystem coordinateSystem)
        {
            if (link == null)
                throw new NullReferenceException();

            return new Line2D(new Point2D(link.FromNode.Latitude, link.FromNode.Longitude) { CoordinateSystem = coordinateSystem },
                new Point2D(link.ToNode.Latitude, link.ToNode.Longitude) { CoordinateSystem = coordinateSystem });
        }

        public static bool InRegion(this Node node, Polygon region, ICoordinateSystem coordinateSystem)
        {
            if (node == null)
                throw new NullReferenceException();
            var point = new Point2D(node.Latitude, node.Longitude) { CoordinateSystem = coordinateSystem };

            var inter = point.Intersection(region, out Location loc);

            return inter != null && (loc == Location.Boundary || loc == Location.Interior);

        }

        public static bool InRegion(this Link link, Polygon region, ICoordinateSystem coordinateSystem)
        {
            if (link == null)
                throw new NullReferenceException();

            var line = new Line2D(new Point2D(link.FromNode.Latitude, link.FromNode.Longitude) { CoordinateSystem = coordinateSystem },
                new Point2D(link.ToNode.Latitude, link.ToNode.Longitude) { CoordinateSystem = coordinateSystem });

            var inter = line.Intersection(region, out Location loc);

            return inter != null && loc == Location.Interior;
        }

        public static Graph SubGraphInRegion(this Graph graph, Polygon region, ICoordinateSystem coordinateSystem)
        {
            if (graph == null)
            {
                throw new NullReferenceException();
            }

            var newGraph = new Graph();
            var indexMap = new SortedDictionary<int, int>();

            foreach (var node in graph.Nodes.Where(n => n.InRegion(region, coordinateSystem)))
            {
                var newNode = new Node()
                {
                    Latitude = node.Latitude,
                    Longitude = node.Longitude,
                };
                newGraph.AddNode(newNode, node.NodeAttributes);
                indexMap.Add(node.Id, newNode.Id);
            }

            foreach (var arc in graph.Arcs.Where(a => a.InRegion(region, coordinateSystem)))
            {
                newGraph.AddLink(indexMap[arc.FromNodeId], indexMap[arc.ToNodeId], arc.LinkData, true);
            }

            foreach (var edge in graph.Edges.Where(e => e.InRegion(region, coordinateSystem)))
            {
                newGraph.AddLink(indexMap[edge.FromNodeId], indexMap[edge.ToNodeId], edge.LinkData, false);
            }

            return newGraph;
        }
    }


}
