using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGAT.Business.Contracts.Services.Algorithms;
using NGAT.Business.Domain.Core;
using NGAT.Geo.Geometries;

namespace NGAT.Services.Algorithms.RobustShortestPath.ScenarioBounded
{
    public class RDSP_Bounded_Scenario : IShortestPathProblemAlgorithm
    {
        public string UniqueId => "RDSP Bounded Scenario";

        public string Description => "Get the Robust Devaition Shortest Path from one node to another. With a bounded number of scenarios. Max scenario's amount = 2. For non multi-scenario graphs runs simple Dijkstra algorithm.";

        public ShortestPathProblemOutput Run(Graph graph, Node sp, Node ep)
        {
            Dictionary<int, IBoundedRDSP> algorithms = new Dictionary<int, IBoundedRDSP>();
            foreach (var imp in NGAT.Services.Utils.GetSubClasses<IBoundedRDSP>())
                algorithms.Add(imp.ScenariosCount, imp);
            if (graph.Scenarios_Count == 1)
            {
                Dijkstra dijktra = new Dijkstra();
                return dijktra.Run(graph, sp, ep);
            }
            else
            {
                if (algorithms.ContainsKey(graph.Scenarios_Count))
                {
                    IBoundedRDSP algorithm = algorithms[graph.Scenarios_Count];
                    return algorithm.Run(graph, sp,ep);
                }
                else
                {
                    throw new Exception("There is no implementation for that amount of scenarios");
                }
            }
        }

        public Task<object> RunAsync(Graph graph, params object[] args)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return UniqueId;
        }
    }
}
