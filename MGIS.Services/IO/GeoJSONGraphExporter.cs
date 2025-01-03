using NGAT.Business.Contracts.IO;
using NGAT.Business.Domain.Core;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NGAT.Services.IO
{
    public class GeoJSONGraphExporter : IGraphExporter
    {
        public string FormatID => "GeoJSON";

        public void Export(Stream stream, Graph graph)
        {
            InternalExport(stream, graph.NodesId);
        }

        public bool ExportPoints { get; set; }

        private void InternalExport(Stream stream, IDictionary<int, Node> nodesIndex)
        {
            var nodes = nodesIndex.Values;
            var markedEdges = new Dictionary<int, bool>();
            using (var tw = new StreamWriter(stream))
            {
                tw.Write("{\"type\":\"FeatureCollection\",\"features\":");
                tw.Write("[");
                List<string> featuresStrings = new List<string>();
                foreach (var node in nodes)
                {

                    var arcs = node.OutgoingArcs;//.Arcs.Where(a => a.FromNodeId == node.Id);//.Where(a => a.FromNode.Latitude <= -82.35806465148926 && a.FromNode.Longitude <= 23.145805714137563 && a.ToNode.Latitude >= -82.37866401672363 && a.ToNode.Longitude >= 23.122363841245967);
                    foreach (var arc in arcs.Where(a => nodesIndex.ContainsKey(a.ToNodeId)))
                    {
                        string attributesToProps = null;
                        if (arc.LinkData != null && arc.LinkData.RawData != null && arc.LinkData.Attributes.Count > 0)
                        {
                            attributesToProps = string.Join(",", arc.LinkData.Attributes.Select(kv => $"\"{kv.Key}\": \"{kv.Value}\""));
                            //attributesToProps = attributesToProps.Substring(0, attributesToProps.Length - 1);
                        }
                        List<string> coordinatesString = new List<string>();
                        if (arc.PointsData != null)
                        {
                            var points = arc.Points;
                        }
                        else
                        {
                            coordinatesString.Add($"[{arc.FromNode.Longitude}, {arc.FromNode.Latitude}]");
                            coordinatesString.Add($"[{arc.ToNode.Longitude}, {arc.ToNode.Latitude}]");
                        }
                        featuresStrings.Add("{ \"type\":\"Feature\",\"properties\":{ \"stroke\": \"#ff0000\", \"stroke-width\": 3, \"stroke-opacity\": 1, \"obj-type\": \"arc\"" + (attributesToProps != null ? ", " + attributesToProps : "") + "},\"geometry\":{ \"type\":\"LineString\",\"coordinates\":[" + string.Join(",", coordinatesString) + "]}}");
                    }
                    foreach (var edge in node.Edges.Where(e => nodesIndex.ContainsKey(e.FromNodeId) && nodesIndex.ContainsKey(e.ToNodeId)))
                    {
                        if (!markedEdges.ContainsKey(edge.Id - 1))
                        {
                            string attributesToProps = null;
                            if (edge.LinkData != null && edge.LinkData.RawData != null && edge.LinkData.Attributes.Count > 0)
                            {
                                attributesToProps = string.Join(",", edge.LinkData.Attributes.Select(kv => $"\"{kv.Key}\": \"{kv.Value}\""));

                            }
                            List<string> coordinatesString = new List<string>();
                            if (edge.PointsData != null)
                            {
                                var points = edge.Points;
                                coordinatesString.AddRange(points.Select(p => $"[{p.X}, {p.Y}]"));
                            }
                            else
                            {
                                coordinatesString.Add($"[{edge.FromNode.Longitude}, {edge.FromNode.Latitude}]");
                                coordinatesString.Add($"[{edge.ToNode.Longitude}, {edge.ToNode.Latitude}]");
                            }
                            featuresStrings.Add("{ \"type\":\"Feature\",\"properties\":{ \"stroke\": \"#00ff00\", \"stroke-width\": 5, \"stroke-opacity\": 1, \"obj-type\": \"edge\"" + (attributesToProps != null ? ", " + attributesToProps : "") + "},\"geometry\":{ \"type\":\"LineString\",\"coordinates\":[" + string.Join(",", coordinatesString) + "]}}");
                            markedEdges[edge.Id - 1] = true;
                        }
                    }
                    if (ExportPoints)
                        featuresStrings.Add("{ \"type\":\"Feature\",\"properties\":{ },\"geometry\":{ \"type\":\"Point\",\"coordinates\":[" + node.Longitude + "," + node.Latitude + "]}}");


                }
                tw.Write(string.Join(",", featuresStrings));
                tw.Write("]");

                tw.Write("}");

                tw.Flush();

            }

        }

        public Task ExportAsync(Stream stream, Graph graph)
        {
            return new Task(() => InternalExport(stream, graph.NodesId));
        }

        public void ExportInRange(Stream stream, double minLat, double MinLong, double maxLat, double MaxLong, Graph graph)
        {
            InternalExport(stream, new SortedDictionary<int, Node>(graph.Nodes.Where(n => n.Latitude >= minLat
                                              && n.Longitude >= MinLong
                                              && n.Latitude <= maxLat
                                              && n.Longitude <= MaxLong).ToDictionary(n => n.Id)));
        }

        public Task ExportInRangeAsync(Stream stream, double minLat, double MinLong, double maxLat, double MaxLong, Graph graph)
        {
            return new Task(() => InternalExport(stream, new SortedDictionary<int, Node>(graph.Nodes.Where(n => n.Latitude >= minLat
                                             && n.Longitude >= MinLong
                                             && n.Latitude <= maxLat
                                             && n.Longitude <= MaxLong).ToDictionary(n => n.Id))));
        }

        public override string ToString()
        {
            return FormatID + " file";
        }

    }

}
