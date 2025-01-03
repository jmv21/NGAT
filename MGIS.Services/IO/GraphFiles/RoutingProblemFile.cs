using DotSpatial.Data;
using DotSpatial.Topology;
using Newtonsoft.Json;
using NGAT.Business.Contracts.IO;
using NGAT.Business.Contracts.Services.Algorithms;
using NGAT.Business.Domain.Core;
using NGAT.Geo.Geometries;
using NGAT.Services.Algorithms;
using NGAT.Services.ForbiddenTurns;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace NGAT.Services.IO.MapFiles
{

    /// <summary>
    /// Represents a file with the input and the output for a Vehicle Routing Problem (VRP).
    /// </summary>
    public class RoutingProblemFile : GraphAlgorithmFile
    {
        /// <summary>
        /// The type of this routing problem file
        /// </summary>
        public RoutingProblemType ProblemType { get; protected set; }
        IShortestPathProblemAlgorithm Algorithm { get; set; }
        ITurnProhibitionsAssociatedGraph TurnProhibitionsGraphModifier { get; set;}
        dynamic input;
        dynamic output;
        readonly List<IFeature> features;
        readonly IEnumerable<string> nodesData;

        /// <summary>
        /// A constructor to export the file.
        /// </summary>
        /// <param name="graph">The graph to run an algorithm and exprot the result.</param>
        /// <param name="mapZoom">The zoom to show in the map.</param>
        /// <param name="latitude">The latitude of the center point to show in the map.</param>
        /// <param name="longitude">The longitude of the center point to show in the map.</param>
        public RoutingProblemFile(Graph graph, RoutingProblemType type, IShortestPathProblemAlgorithm algorithm = null, ITurnProhibitionsAssociatedGraph turnProhibitionsGraphModifier = null, List<IFeature> Features = null, IEnumerable<string> nodesData = null) : base("RPF", null, graph)
        {
            ProblemType = type;
            this.features = Features;
            this.nodesData = nodesData;
            this.TurnProhibitionsGraphModifier = turnProhibitionsGraphModifier;
            this.Algorithm = algorithm;
            Initialize();
        }

        /// <summary>
        /// A constructor to import the file.
        /// </summary>
        public RoutingProblemFile() : base("RPF", null, null)
        {
            Initialize();
        }

        public override void Export(string filePath)
        {
            if (Graph == null || Input == null) throw new InvalidOperationException();
            var stream = new FileStream(filePath, FileMode.CreateNew);
            using (var writer = new StreamWriter(stream))
            {
                // First, export the graph:
                // Third line: n <=> Graph.Nodes.Count
                writer.WriteLine("Nodes");
                writer.WriteLine(Graph.Nodes.Count);
                // Next n lines, for each node: node.Id (space) node.Lat (space) node.Long
                foreach (var node in Graph.Nodes)
                {
                    writer.WriteLine(node.Id + " " + node.Latitude + " " + node.Longitude);
                }

                // Next line: n <=> Graph.Edges.Count
                writer.WriteLine("Edges");
                writer.WriteLine(Graph.Edges.Count);
                // Next n lines, for each edge: edge.Id (space) edge.FromNode.Id (space) edge.ToNode.Id
                foreach (var edge in Graph.Edges)
                {
                    writer.WriteLine(edge.Id + " " + edge.FromNodeId + " " + edge.ToNodeId);
                }

                // Next line: n <=> Graph.Arcs.Count
                writer.WriteLine("Arcs");
                writer.WriteLine(Graph.Arcs.Count);
                // Next n-th lines, for each arc: arc.Id (space) arc.FromNode.Id (space) arc.ToNode.Id
                foreach (var arc in Graph.Arcs)
                {
                    writer.WriteLine(arc.Id + " " + arc.FromNodeId + " " + arc.ToNodeId);
                }

                // The type of routing problem
                writer.WriteLine("Type");
                writer.WriteLine(ProblemType);                

                // Export the input
                writer.WriteLine("Input");

                if (ProblemType == RoutingProblemType.VRP || ProblemType == RoutingProblemType.NRP)
                {

                    // Here, Input is List<int>
                    input = Input;
                    // Next line, N <=> input.Count : node1.Id (space) node2.Id (space) ... (space) nodeN.Id
                    foreach (int nodeID in input)
                    {
                        writer.Write(nodeID + " ");
                    }
                    writer.WriteLine();

                    // Export addresses of input points
                    if (this.nodesData != null)
                    {
                        Dictionary<int, string> addrs = new Dictionary<int, string>();
                        int index = 0;
                        foreach (var data in nodesData)
                        {
                            addrs.Add(input[index], data);
                            index++;
                        }
                        writer.WriteLine(JsonConvert.SerializeObject(addrs));
                    }
                    

                    

                    // Export the matrix node to node with the best road.
                    writer.WriteLine("Distances");
                    writer.Write("(");
                    for (int i = 0; i < input.Count; i++)
                    {
                        writer.Write("(");
                        Node sp = Graph.NodesId[input[i]];
                        for (int j = 0; j < input.Count; j++)
                        {
                            if(i == j) writer.Write("0 ");
                            else
                            {
                                //Compute the best road from Nodes[i] to Nodes[j]
                                Node ep = Graph.NodesId[input[j]];
                                if(ProblemType == RoutingProblemType.NRP)
                                {
                                    Graph dual = AlgorithmUtils.NonDirectedGraph(Graph);
                                    sp = dual.NodesId[input[i]];
                                    ep = dual.NodesId[input[j]];
                                    ShortestPathProblemOutput result = (ShortestPathProblemOutput)Algorithm.Run(dual, sp, ep);
                                    writer.Write((int)result.Distance + " ");
                                }
                                else
                                {
                                    if (TurnProhibitionsGraphModifier == null)
                                    {
                                        ShortestPathProblemOutput result = (ShortestPathProblemOutput)Algorithm.Run(Graph, sp, ep);
                                        writer.Write((int)result.Distance + " ");
                                    }
                                    else
                                    {
                                        Graph dual = TurnProhibitionsGraphModifier.DualGraphBuilder(Graph, sp.Id, ep.Id);
                                        sp = dual.NodesId[TurnProhibitionsGraphModifier.dualStartNodeId];
                                        ep = dual.NodesId[TurnProhibitionsGraphModifier.dualEndNodeId];
                                        ShortestPathProblemOutput result = (ShortestPathProblemOutput)Algorithm.Run(dual, sp, ep);
                                        writer.Write((int)result.Distance + " ");
                                        sp = Graph.NodesId[input[i]];
                                    }

                                }

                                //writer.WriteLine(PathToJson(result.NodesId));
                                //result = (DijkstraOutPut)dijkstra.Run(Graph, ep, sp, new ClassicForbiddenTurnFilter());
                                //writer.WriteLine("{0} {1} {2}", input[j], input[i], result.Distance);
                                //writer.WriteLine(PathToJson(result.NodesId));
                            }

                        }
                        writer.WriteLine(")");
                    }
                    writer.Write(")");
                }
                else
                {
                    //Here, Input is a node ID                    
                    writer.WriteLine(Input);
                }


                writer.WriteLine("\nOutput");
                // Them, export the output: here, Output is List<List<int>>, each line represents the route of each vehicle
                if (Output != null && output.Count > 0)
                {
                    // Next line, N <=> output.Count : node1.Id (space) node2.Id (space) ... (space) nodeN.Id
                    foreach (List<int> route in output)
                    {
                        foreach (var nodeID in route)
                            writer.Write(nodeID + " ");
                        writer.WriteLine();
                    }
                }
            }
            stream.Close();
        }

        public override void Import()
        {
            if(FileUri == null) throw new InvalidOperationException("The uri of the file cannot be null.");
            var stream = new FileStream(FileUri.LocalPath, FileMode.Open);
            using (var reader = new StreamReader(stream))
            {
                // Read the graph
                Graph = new Graph();
                reader.ReadLine(); // skip Nodes label
                int nodesCount = int.Parse(reader.ReadLine());
                for (int i = 0; i < nodesCount; i++)
                {
                    var line = reader.ReadLine().Split();
                    Node node = new Node() { Id = int.Parse(line[0]), Latitude = double.Parse(line[1].Replace(',', '.'), CultureInfo.InvariantCulture), Longitude = double.Parse(line[2].Replace(',', '.'), CultureInfo.InvariantCulture) };
                    Graph.AddNode(node, new Dictionary<string, string>());
                }
                reader.ReadLine(); // skip Edges label
                int edgesCount = int.Parse(reader.ReadLine());
                for (int i = 0; i < edgesCount; i++)
                {
                    var edge = reader.ReadLine().Split();
                    Graph.AddLink(int.Parse(edge[1]), int.Parse(edge[2]), null, false);
                }
                reader.ReadLine(); // skip Arcs label
                int arcsCount = int.Parse(reader.ReadLine());
                for (int i = 0; i < arcsCount; i++)
                {
                    var arc = reader.ReadLine().Split();
                    Graph.AddLink(int.Parse(arc[1]), int.Parse(arc[2]), null, true);
                }

                //Read the type
                reader.ReadLine(); // skip Type label
                string type = reader.ReadLine();
                ProblemType = type == "VRP" ? RoutingProblemType.VRP : RoutingProblemType.ARP;

                //Read the input
                reader.ReadLine(); // skip Input label

                if(ProblemType == RoutingProblemType.VRP)
                {
                    var inputNodes = reader.ReadLine().Split();
                    Input = new List<int>();
                    input = Input;
                    for (int i = 0; i < inputNodes.Length - 1; i++)
                    {
                        input.Add(int.Parse(inputNodes[i]));
                    }

                    // skip matrix
                    while (reader.ReadLine() != "Output")
                    {
                        // skip line until find "Output".
                    }
                }
                else
                {
                    Input = int.Parse(reader.ReadLine());
                    reader.ReadLine(); // skip label
                }

                // Read the output
                Output = new List<List<int>>();
                output = Output;
                while (!reader.EndOfStream)
                {
                    string[] route = reader.ReadLine().Split();
                    output.Add(new List<int>());
                    for (int i = 0; i < route.Length; i++)
                        if (int.TryParse(route[i], out int id))
                            output[output.Count - 1].Add(id);
                }
                
            }
        }

        void Initialize()
        {
            Input = new List<int>();
            Output = new List<List<int>>();
            output = Output;
        }

        string PathToJson(IEnumerable<int> nodesId)
        {
            Dictionary<int, string> path = new Dictionary<int, string>();
            IFeature feature1 = (features == null) ? null : features[0];
            foreach (var id in nodesId) // the nodes of the path
            {
                Point point = new Point(Graph.NodesId[id].Longitude, Graph.NodesId[id].Latitude);
                if (feature1 != null && !feature1.Contains(point))
                {
                    foreach (var feature in features) // the polygons that represent the municipalities
                        if (feature.Contains(point))
                        {
                            feature1 = feature;
                            break;
                        }
                }
                string name = (feature1 == null) ? "-" : (string)feature1.DataRow.ItemArray[6];
                path.Add(id, name);
            }
            return JsonConvert.SerializeObject(path);
        }


    }

}
