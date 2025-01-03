using NGAT.Business.Contracts.IO;
using NGAT.Business.Domain.Core;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NGAT.GraphGenerator.Services.IO
{
    public class GRFGraphExporter : IGraphExporter
    {
        #region IGraphExporter Members
        public string FormatID => "GRF";

        public void Export(Stream stream, Graph graph)
        {
            InternalExport(stream, graph.NodesId);
        }

        public Task ExportAsync(Stream stream, Graph graph)
        {
            return new Task(() => InternalExport(stream, graph.NodesId));
        }

        private void InternalExport(Stream stream, IDictionary<int, Node> nodesIndex)
        {
            var nodes = nodesIndex.Values.ToArray();
            var markedEdges = new Dictionary<int, bool>();
            using (var tw = new StreamWriter(stream))
            {
                tw.WriteLine("GRAFO");//First line
                int graphType = nodes.Any(n => (n.Degree > 0 && (n.InDegree > 0 || n.OutDegree > 0)))
                            ? 2 // Mixed Graph
                            : (nodes.Any(n => n.InDegree > 0 || n.OutDegree > 0)
                            ? 1 // Directed Graph
                            : 3); // Undirected Graph
                tw.WriteLine(graphType);

                //Writing nodes (First pass)
                for (int i = 0; i < nodes.Length; i++)
                {

                    int last = i == nodes.Length - 1 ? 1 : 0;
                    tw.WriteLine($"{nodes[i].Id} {nodes[i].Latitude} {nodes[i].Longitude} {last}");
                }

                //Writing links (Second Pass)
                foreach (var node in nodes)
                {
                    foreach (var edge in node.Edges)
                    {
                        if (!markedEdges.ContainsKey(edge.Id) && nodesIndex.ContainsKey(edge.FromNodeId) && nodesIndex.ContainsKey(edge.ToNodeId))
                        {
                            markedEdges[edge.Id] = true;
                            string isMultiScenario = edge.Distance.IsMultiScenario ? "1" : "0";
                            string distances = "";
                            if(edge.Distance.IsMultiScenario)
                            {
                                foreach(var distance in edge.Distance.Costs)
                                    distances = distances + distance.ToString()+" ";
                            }

                            tw.WriteLine($"{edge.FromNodeId} {edge.ToNodeId} {edge.Distance.cost.ToString()} {1} {isMultiScenario} {edge.Distance.Costs.Count} 0 {distances}{edge.LinkData.RawData}");
                        }
                    }
                    foreach (var arc in node.OutgoingArcs)
                    {
                        if (nodesIndex.ContainsKey(arc.ToNodeId))
                        {
                            string isMultiScenario = arc.Distance.IsMultiScenario ? "1" : "0";
                            string distances = "";
                            if (arc.Distance.IsMultiScenario)
                            {
                                foreach (var distance in arc.Distance.Costs)
                                    distances = distances + distance.ToString() + " ";
                            }
                            tw.WriteLine($"{arc.FromNodeId} {arc.ToNodeId} {arc.Distance.cost.ToString()} {0} {isMultiScenario} {arc.Distance.Costs.Count} 0 {distances}{arc.LinkData.RawData}");
                        }
                    }
                }
                tw.Flush();
            }
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
        #endregion

        public override string ToString()
        {
            return FormatID + " file";
        }

    }

}
