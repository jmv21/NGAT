using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using NGAT.Business.Domain.Core;
using NGAT.Geo;
using NGAT.Geo.Geometries;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NGAT.Services.ResultDisplayers;

namespace NGAT.WindowsAPI
{
    public static class GMapExtensions
    {
        /// <summary>
        /// Gets the envelop shown in the map.
        /// </summary>
        /// <param name="gMap">The GMapControl to calculate its envelop.</param>
        /// <returns>The envelop shown in the map.</returns>
        public static Envelope GetEnvelope(this GMapControl gMap)
        {
            PointLatLng min = gMap.FromLocalToLatLng(0, gMap.Height);
            PointLatLng max = gMap.FromLocalToLatLng(gMap.Width, 0);
            return new Envelope(new Coordinate(min.Lat, min.Lng) { CoordinateSystem = new GeoCoordinateSystem()},
                                new Coordinate(max.Lat, max.Lng) { CoordinateSystem = new GeoCoordinateSystem() });
        }

        /// <summary>
        /// Displays in the map the minimun rectangle which contains the indicated region.
        /// </summary>
        /// <param name="gMap">The GMapControl reference.</param>
        /// <param name="region">The region to display.</param>
        public static void ShowRegion(this GMapControl gMap, Polygon region)
        {
            double maxLat = region.Envelope.MaxCoordinate[0];
            double maxLong = region.Envelope.MaxCoordinate[1];
            double minLat = region.Envelope.MinCoordinate[0];
            double minLong = region.Envelope.MinCoordinate[1];

            gMap.Position = new PointLatLng((maxLat + minLat) / 2, (maxLong + minLong) / 2);
            //gMap.Refresh();

            Envelope mapEnvelope = gMap.GetEnvelope();
            while (mapEnvelope.Contains(region.Envelope))
            {
                gMap.Zoom += 0.25;
                mapEnvelope = gMap.GetEnvelope();
                if (gMap.Zoom >= gMap.MaxZoom)
                {
                    gMap.Zoom = gMap.MaxZoom;
                    //gMap.Refresh();
                    return;
                }
            }
        }

        /// <summary>
        /// Displays in the map the minimun rectangle which contains the graph
        /// </summary>
        /// <param name="gMap"></param>
        /// <param name="graph"></param>
        public static void ShowRegion(this GMapControl gMap, Graph graph)
        {
            double minLat = double.MaxValue, minLng = double.MaxValue, maxLat = double.MinValue, maxLng = double.MinValue;
            foreach (var node in graph.Nodes)
            {
                if (node.Latitude < minLat) minLat = node.Latitude;
                if (node.Latitude > maxLat) maxLat = node.Latitude;
                if (node.Longitude < minLng) minLng = node.Longitude;
                if (node.Longitude > maxLng) maxLng = node.Longitude;
            }
            gMap.SetZoomToFitRect(new RectLatLng(minLat, minLng, maxLng - minLng, maxLat - minLat));
            gMap.Position = new PointLatLng((maxLat + minLat) / 2, (maxLng + minLng) / 2);
        }

        /// <summary>
        /// Displays in the map the region centered in <paramref name="center"/> with specified <paramref name="zoom"/>.
        /// </summary>
        /// <param name="gMap">The GMapControl reference.</param>
        /// <param name="zoom">The map zoom.</param>
        /// <param name="center">The map center position.</param>
        public static void ShowRegion(this GMapControl gMap, double zoom, PointLatLng center)
        {
            gMap.Zoom = zoom;
            gMap.Position = center;
        }


        /// <summary>
        /// Gets a map overlay which contains all nodes from the given graph located in the given envelope
        /// </summary>
        /// <param name="gMap"></param>
        /// <param name="graph"></param>
        /// <param name="envelope"></param>
        /// <returns></returns>
        public static GMapOverlay GetNodesOverlay(this GMapControl gMap, Graph graph, Envelope envelope)
        {
            GMapOverlay nodesOverlay = new GMapOverlay("nodes");
            //Add a marker for each node located in the envelope
            foreach (var node in graph.Nodes.Where(n => envelope.IntersectsWith(n.Coordinate)))
            {
                GMapMarker marker = new CustomMarker(new PointLatLng(node.Latitude, node.Longitude), new Bitmap("Resources\\BitMaps\\red_point.png"), node.Id.ToString());
                //var marker = new GMarkerGoogle(new PointLatLng(node.Latitude, node.Longitude), new Bitmap("Resources\\BitMaps\\red_point.png"));
                marker.Tag = node.Id;
                marker.ToolTipText = node.Id.ToString(); // for experiments
                nodesOverlay.Markers.Add(marker);
            }

            return nodesOverlay;
        }

        /// <summary>
        ///  Gets a map overlay which contains all nodes from the given graph located in the given polygon
        /// </summary>
        /// <param name="gMap"></param>
        /// <param name="graph"></param>
        /// <param name="polygon"></param>
        /// <returns></returns>
        public static GMapOverlay GetNodesOverlay(this GMapControl gMap, Graph graph, Polygon polygon)
        {
            GMapOverlay nodesOverlay = new GMapOverlay("nodes");
            //Add a marker for each node contained in the polygon
            foreach (var node in graph.Nodes.Where(n => polygon.Envelope.IntersectsWith(n.Coordinate)))
            {
                GMapMarker marker = new CustomMarker(new PointLatLng(node.Latitude, node.Longitude), new Bitmap("Resources\\BitMaps\\red_point.png"), node.Id.ToString());
                //var marker = new GMarkerGoogle(new PointLatLng(node.Latitude, node.Longitude), new Bitmap("Resources\\BitMaps\\red_point.png"));
                marker.Tag = node.Id;
                marker.ToolTipText = node.Id.ToString(); // for experiments
                nodesOverlay.Markers.Add(marker);
            }

            return nodesOverlay;
        }

        /// <summary>
        /// Gets a map overlay which contains all edges from the given graph located in the given envelope
        /// </summary>
        /// <param name="gMap"></param>
        /// <param name="graph"></param>
        /// <param name="envelope"></param>
        /// <returns></returns>
        public static GMapOverlay GetEdgesOverlay(this GMapControl gMap, Graph graph, Envelope envelope)
        {
            GMapOverlay edgesOverlay = new GMapOverlay("edges");
            // Add a two points route for each edges which nodes are located in the envelope
            foreach (var edge in graph.Edges.Where(e => envelope.IntersectsWith(e.FromNode.Coordinate) && envelope.IntersectsWith(e.ToNode.Coordinate)))
            {
                var route = new GMapRoute(new PointLatLng[] { new PointLatLng(edge.FromNode.Latitude, edge.FromNode.Longitude),
                                                       new PointLatLng(edge.ToNode.Latitude, edge.ToNode.Longitude)}, "edge");
                route.Stroke.Color = Color.IndianRed;
                edgesOverlay.Routes.Add(route);
            }

            return edgesOverlay;
        }

        /// <summary>
        /// Gets a map overlay which contains all edges from the given graph located in the given polygon
        /// </summary>
        /// <param name="gMap"></param>
        /// <param name="graph"></param>
        /// <param name="polygon"></param>
        /// <returns></returns>
        public static GMapOverlay GetEdgesOverlay(this GMapControl gMap, Graph graph, Polygon polygon)
        {
            GMapOverlay edgesOverlay = new GMapOverlay("edges");
            var envelope = polygon.Envelope;
            // Add a two points route for each edges which nodes are located in the envelope
            foreach (var edge in graph.Edges.Where(e=>envelope.IntersectsWith(e.FromNode.Coordinate)&&envelope.IntersectsWith(e.ToNode.Coordinate)))
            {
                var route = new GMapRoute(new PointLatLng[] { new PointLatLng(edge.FromNode.Latitude, edge.FromNode.Longitude),
                                                       new PointLatLng(edge.ToNode.Latitude, edge.ToNode.Longitude)}, "edge");
                route.Stroke.Color = Color.IndianRed;
                edgesOverlay.Routes.Add(route);
            }

            return edgesOverlay;
        }

        /// <summary>
        /// Gets a map overlay which contains all arcs from the given graph located in the given envelope
        /// </summary>
        /// <param name="gMap"></param>
        /// <param name="graph"></param>
        /// <param name="envelope"></param>
        /// <returns></returns>
        public static GMapOverlay GetArcsOverlay(this GMapControl gMap, Graph graph, Envelope envelope)
        {
            GMapOverlay arcsOverlay = new GMapOverlay("arcs");
            // Add a two points route for each edges which nodes are located in the envelope
            foreach (var edge in graph.Arcs.Where(e=>envelope.IntersectsWith(e.FromNode.Coordinate)&&envelope.IntersectsWith(e.ToNode.Coordinate)))
            {
                var route = new GMapRoute(new PointLatLng[] { new PointLatLng(edge.FromNode.Latitude, edge.FromNode.Longitude),
                                                       new PointLatLng(edge.ToNode.Latitude, edge.ToNode.Longitude)}, "arc");
                route.Stroke.Color = Color.IndianRed;
                arcsOverlay.Routes.Add(route);
            }

            return arcsOverlay;
        }

        /// <summary>
        /// Gets a map overlay which contains all arcs from the given graph located in the given polygon.
        /// </summary>
        /// <param name="gMap"></param>
        /// <param name="graph"></param>
        /// <param name="polygon"></param>
        /// <returns></returns>
        public static GMapOverlay GetArcsOverlay(this GMapControl gMap, Graph graph, Polygon polygon)
        {
            GMapOverlay arcsOverlay = new GMapOverlay("arcs");
            var envelope = polygon.Envelope;
            // Add a two points route for each edges which nodes are located in the envelope
            foreach (var edge in graph.Arcs.Where(e=>envelope.IntersectsWith(e.FromNode.Coordinate)&&envelope.IntersectsWith(e.ToNode.Coordinate)))
            {
                var route = new GMapRoute(new PointLatLng[] { new PointLatLng(edge.FromNode.Latitude, edge.FromNode.Longitude),
                                                       new PointLatLng(edge.ToNode.Latitude, edge.ToNode.Longitude)}, "arc");
                route.Stroke.Color = Color.IndianRed;
                arcsOverlay.Routes.Add(route);
            }

            return arcsOverlay;
        }

        /// <summary>
        /// Gets a map overlay from a Layer instance.
        /// </summary>
        /// <param name="gMap"></param>
        /// <param name="layer"></param>
        /// <returns>The new overlay</returns>
        public static GMapOverlay GetOverlay(this GMapControl gMap, Layer layer)
        {
            GMapOverlay newLayer = new GMapOverlay(layer.Name);
            foreach (var item in layer.Geometries)
            {
                switch (item.GeometryType)
                {
                    case GeometryType.Point: // for points
                        var marker = new GMarkerGoogle(new PointLatLng(item.Coordinates[0][0], item.Coordinates[0][1]), new Bitmap(layer.IconPath));
                        marker.ToolTipText = layer.Name;
                        newLayer.Markers.Add(marker);
                        break;
                    case GeometryType.Polygon: // for polygons
                        List<PointLatLng> points = new List<PointLatLng>();
                        foreach (var coord in item.Coordinates)
                            points.Add(new PointLatLng(coord[0], coord[1]));
                        GMapPolygon polygon = new GMapPolygon(points, layer.Name);
                        newLayer.Polygons.Add(polygon);
                        break;
                    case GeometryType.LineString: // for routes
                        points = new List<PointLatLng>();
                        foreach (var coord in item.Coordinates)
                            points.Add(new PointLatLng(coord[0], coord[1]));
                        GMapRoute route = new GMapRoute(points, layer.Name);
                        newLayer.Routes.Add(route);
                        break;
                    default:
                        break;
                }
            }
            //gMap.Overlays.Add(newLayer);
            //newLayer.IsVisibile = true;
            gMap.Refresh();
            return newLayer;
        }
    }
}
