using NGAT.Business.Contracts.IO;
using NGAT.Business.Contracts.Services.Algorithms;
using NGAT.Services.Algorithms;
using NGAT.Services.IO;
using NGAT.Services.IO.Osm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NGAT.Services.IO.MapFiles;
using NGAT.Geo.Geometries;
using OsmSharp;
using OsmSharp.Streams;
using NGAT.Services.Algorithms.RobustShortestPath;
using NGAT.Business.Domain.Core;
using System.Diagnostics;
using Node = NGAT.Business.Domain.Core.Node;
using NGAT.Services.Algorithms.TurnProhibitionsGraphs;
using NGAT.Services.Algorithms.RobustShortestPath.ScenarioBounded;

namespace NGAT.ConsoleTesting
{
    class Program
    {
        static List<IGraphBuilder> Builders = new List<IGraphBuilder>()
        {
            new OsmPbfGraphBuilder(new LinkFiltrerCollection(), new AttributesFetcherCollection(), new AttributesFetcherCollection()),
            new GRFGraphBuilder()
        };
        static List<IGraphExporter> Exporters = new List<IGraphExporter>()
        {
            new GeoJSONGraphExporter() {ExportPoints = true},
            new GRFGraphExporter()
        };

        static int Main(string[] args)
        {
            GRFGraphBuilder graphBuilder = new GRFGraphBuilder();
            string[] paths = AllFiles("D:\\Shool\\TESIS\\Casos de prueba\\Datos Simulados\\Grafos Para Algoritmos Robustos\\Muestra");
            string path = "D:\\Shool\\TESIS\\Casos de prueba\\Datos Simulados\\RESULTADOS\\All Data\\Robust\\RDSP_Bounded_Scenario.txt";
            double promedio = 0;

                for (int i = 0; i < paths.Length; i++)
                {   
                    Uri uri = new Uri(Path.GetFullPath(paths[i]));
                    Graph graph = graphBuilder.Build(uri);
                    double density = 0.5;
                
                //(long, long) resultARSPBranchAndBound = Robust(graph, i, new BruteForceRDSP());
                //Console.WriteLine("BanchAndBoundRDSP - " + resultARSPBranchAndBound);
                (long, long) resultARSPBounded = Robust(graph, i, new RDSP_Bounded_Scenario());
                Console.WriteLine("RDSP_Bounded_Scenario - " + resultARSPBounded);
                //(long, double, int, long, int) resultSplit = Method(graph, density, i, new SplitProhibitionsGraph());
                //Console.WriteLine("Split - "+resultSplit);
                //(long, double, int,long, int) resultTurnNet = Method(graph, density, i, new TurnNetProhibitionsGraph());
                //Console.WriteLine("TurnNet - " + resultTurnNet);
                using (StreamWriter escritor = new StreamWriter(path, true))
                    {
                        escritor.WriteLine(uri.OriginalString.Split('\\').Last());
                    //escritor.WriteLine("BranchAndBoundRDSP Time: " + resultARSPBranchAndBound.Item1 + " Memory: " + resultARSPBranchAndBound.Item2);
                    escritor.WriteLine("BoundedRDSP Time: " + resultARSPBounded.Item1 + " Memory: " + resultARSPBounded.Item2);
                    //escritor.WriteLine("Split - Number of Turns: " + resultSplit.Item3 + " TurnProhibitions: " + resultSplit.Item5+ " Time: " +resultSplit.Item1.ToString() + " Memory -" + resultSplit.Item4);
                    //escritor.WriteLine("TurNet - Number of Turns: " + resultTurnNet.Item3 + " TurnProhibitions: "+ resultTurnNet.Item5 + " Time: " +resultTurnNet.Item1.ToString() + " Memory:" + resultTurnNet.Item4);
                }
                }

            //Uri uri = new Uri(Path.GetFullPath("D:\\Shool\\TESIS\\Casos de prueba\\Datos Simulados\\Grafos Para Algoritmos de Prohibiciones de Giro\\N-250 D-0.75\\graph1.grf"));
            //Graph graph = graphBuilder.Build(uri);
            //Console.WriteLine(Method(graph, 0.00005, 8, new TurnNetProhibitionsGraph()));
            return 0;
        }

        static string [] AllFiles (string path)
        {
            string [] files = Directory.GetFiles(path);
            return files;
        }

        private static List<(ShortestPathProblemOutput,long)> Solver(IShortestPathProblemAlgorithm algorithm, List<Graph> graphs, List<(int,int)> starAndEndPoints)
        {
            Stopwatch sw = new Stopwatch();
            List<(ShortestPathProblemOutput,long)> results = new List<(ShortestPathProblemOutput,long)>();
            for (int i = 0; i < graphs.Count; i++)
            {
                sw.Start();
                ShortestPathProblemOutput result = (ShortestPathProblemOutput)algorithm.Run(graphs[i],graphs[i].NodesId[starAndEndPoints[i].Item1], graphs[i].NodesId[starAndEndPoints[i].Item2]);
                sw.Stop();
                results.Add((result,sw.ElapsedMilliseconds));
                sw.Reset();
            }
            return results;
        }
       /* private static void VRPSolving(string inputFile, string outputFile)
        {
            RoutingProblemFile file = new RoutingProblemFile();
            file.FileUri = new Uri(Path.GetFullPath(inputFile));
            Console.WriteLine("Importing File ...");
            file.Import();
            dynamic output = file.Output;
            IShortestPathProblemAlgorithm dijkstra = new Dijkstra();
            var input = (List<int>)file.Input;
            for (int i = 0; i < input.Count - 1; i++)
            {
                var start = new PointLatLong(file.Graph.NodesId[input[i]].Latitude, file.Graph.NodesId[input[i]].Longitude);
                var end = new PointLatLong(file.Graph.NodesId[input[i + 1]].Latitude, file.Graph.NodesId[input[i + 1]].Longitude);
                dynamic result = dijkstra.Run(file.Graph, start, end);
                foreach (var item in result.NodesId)
                    if(output.Count == 0 || output[output.Count - 1] != item) output.Add(item);

            }
            Console.WriteLine("Exporting File ...");
            file.Export(outputFile);

        }*/

        /*private static IShortestPathProblemAlgorithm ResolveAlgorithm(string algorithmID)
        {
            switch (algorithmID)
            {
                case "floydwarshall":
                    return new FloydWarshall();
                case "connected":
                    return new CheckConectivity();
                default:
                    return null;
            }

        }*/

        static void PrintUsage(string errorMsg = "")
        {
            if (!string.IsNullOrWhiteSpace(errorMsg))
                Console.WriteLine(errorMsg);
            Console.WriteLine();
            Console.WriteLine("NGAT Tools usage:");
            Console.WriteLine("NGATTools <input-file> <output-file> [--in-range minLat minLon maxLat1 maxLon] [--fna attr1 attr2 ...] [--fla attr1 attr2] [--lf key key1=value1 key2!=value2 ...]");
            Console.WriteLine();
            Console.WriteLine("     Optional Parameters ([]):");
            Console.WriteLine("         --in-range minLat minLon maxLat maxLon:             Exports only the subgraph contained in the region specified by min and max coordinates");
            Console.WriteLine("         --fna attr1 attr2...:                                (F)etch (N)ode (A)ttributes, fetches the specified node attributes");
            Console.WriteLine("         --fla attr1 attr2...:                                (F)etch (L)ink (A)ttributes, fetches the specified link attributes");
            Console.WriteLine("         --lf key key1=value1 key2!=value2:                   (L)ink (F)ilters. Filters links based on its attributes. When '=' or '!=' not specified, it checks if link contains attribute only, otherwise, it checks if it contains the attribute and if it's equal('=') or not equal('!=') to a specified value");

        }


        public static void FirstMethod (string path)
        {

        }
        public static (long,long) Robust(Graph graph,int randomSeed, IShortestPathProblemAlgorithm algorithm)
        {
            Random random = new Random(randomSeed);
            int start = random.Next(1, graph.NodesMaxId);
            int end = (start + random.Next(1, graph.Nodes.Count)) % graph.Nodes.Count;
            if (end == 0)
                end = graph.NodesMaxId;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ShortestPathProblemOutput result = algorithm.Run(graph, graph.NodesId[start], graph.NodesId[end]);
            stopwatch.Stop();
            long memory = Process.GetCurrentProcess().PrivateMemorySize64;
            return(stopwatch.ElapsedMilliseconds,memory);
        }
        public static (long, double, int, long, int) Method (Graph graph, double tpDensity,int randomSeed, ITurnProhibitionsAssociatedGraph algorithm)
        {
            Random random = new Random(randomSeed);
            int numberOfTurns = 0;
            List<List<List<bool>>> usedTurn = new List<List<List<bool>>>();
            foreach (Node node in graph.Nodes)
            {
                List<List<bool>> nodeTurns = new List<List<bool>>();
                for (int i = 0; i < node.IncomingArcs.Count; i++)
                {
                    List<bool> nodeTurnsIncomingJ = new List<bool>();
                    for (int j = 0; j < node.OutgoingArcs.Count; j++)
                    {
                        nodeTurnsIncomingJ.Add(false);
                    }
                    nodeTurns.Add(nodeTurnsIncomingJ);
                }
                usedTurn.Add(nodeTurns);
                numberOfTurns = numberOfTurns + (node.IncomingArcs.Count*node.OutgoingArcs.Count);
            }
            int TurnsToBan = (int)(numberOfTurns * tpDensity);
            for (int i = 0; i < TurnsToBan; i++)
            {
                int turnNumber = random.Next(1,numberOfTurns);
                for (int j = 0; j < usedTurn.Count; j++)
                {
                    bool next = false;
                    for (int k = 0; k < usedTurn[j].Count; k++)
                    {
                        bool finded = false;
                        for (int l = 0; l < usedTurn[j][k].Count; l++)
                        {
                            if (turnNumber == 0)
                            {
                                if (!usedTurn[j][k][l])
                                {
                                    usedTurn[j][k][l] = true;
                                    graph.TurnProhibitions.Add((graph.Nodes[j].IncomingArcs[k].FromNode, graph.Nodes[j], graph.Nodes[j].OutgoingArcs[l].ToNode));
                                    finded = true;
                                    break;
                                }
                            }
                            else if (usedTurn[j][k][l] == false)
                                turnNumber--;
                        }
                        if (finded)
                        {
                            next = true;
                            break;
                        }
                    }
                    if (next)
                        break;
                }
            }
            int start = random.Next(1,graph.NodesMaxId);
            int end = (start + random.Next(1,graph.Nodes.Count))%graph.Nodes.Count;
            if (end ==0)
                end = graph.NodesMaxId;
            Console.WriteLine("Starting the algortihm");
            Dijkstra dijkstra = new Dijkstra();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ShortestPathProblemOutput result = dijkstra.Run(algorithm.DualGraphBuilder(graph, start, end), algorithm.DualGraph.NodesId[algorithm.dualStartNodeId], algorithm.DualGraph.NodesId[algorithm.dualEndNodeId]);
            stopwatch.Stop();
            long memory = Process.GetCurrentProcess().PrivateMemorySize64;
            graph.TurnProhibitions.Clear();
            return (stopwatch.ElapsedMilliseconds,result.Distance,numberOfTurns, memory, TurnsToBan);
        }
        /// <summary>
        /// Generate a random Digraph
        /// </summary>
        /// <param name="nodes_count"> The number of nodes of the Digraph</param>
        /// <param name="number_of_scenarios"></param>
        /// <param name=""></param>
        /// <returns></returns>
    }
}
