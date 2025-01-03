using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGAT.Business.Domain.Core;
using NGAT.GraphGenerator.Business.Contracts;
using NGAT.Geo;
namespace NGAT.GraphGenerator.Services.GraphGenerators
{
    public class RandomGraphGenerator : IGraphGenerator
    {
        public int numberOfScenarios { get; set; }
        public int nodesAmount { get; set; }
        public int arcsAmount { get; set; }
        public int seed { get; set; }
        public double maxCost { get; set; }
        public double minCost { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
        public RandomGraphGenerator()
        {
            this.Id = "Graph Generator";
            this.Description = "Generate a random Graph. Parameters:\nNodes amount\nArcs amount\nSeed\nArc Min Cost\nArc Max Cost";
        }

        public Graph Generate()
        {
            Random random = new Random(seed);
            Graph graph = new Graph();
            for (int i = 0; i < nodesAmount; i++)
            {
                graph.AddNode(0, 0, new Dictionary<string, string>());
            }
            List<int> arcs = new List<int>();
            for (int i = 0; i < nodesAmount * (nodesAmount - 1); i++)
            {
                arcs.Add(i);
            }
            for (int i = 0; i < arcsAmount; i++)
            {
                int arcIndex = random.Next(arcs.Count);
                int arc = arcs[arcIndex];
                arcs.RemoveAt(arcIndex);
                int v1 = arc / (nodesAmount - 1);
                int v2 = arc % (nodesAmount - 1);
                if (v2 >= v1)
                {
                    v2++;
                }
                List<double> costs = new List<double>();
                for (int j = 0; j < numberOfScenarios; j++)
                {
                costs.Add(random.Next((int)minCost,(int)maxCost) + random.NextDouble());
                }
                graph.AddLink(v1 + 1, v2 + 1,new Distance(costs, costs.Count > 1 ? true : false), new LinkData(), true);
            }
            seed++;
            return graph;
        }

        public IEnumerable<Graph> GenerateAll(int amount)
        {

            for (int i = 0; i < amount; i++)
            {
                yield return Generate();
            }
        }

        public List<(Type, string)> UsedParameters()
        {
            return new List<(Type, string)> { (typeof(int), "Nodes amount"), (typeof(int), "Arcs amount"), (typeof(int), "seed"), (typeof(int), "Min arc cost"), (typeof(int), "Max arc cost"), (typeof(int),"Number of scenarios") };
        }

        public void SetParameters(object[] args)
        {
            nodesAmount = (int)args[0];
            arcsAmount = (int)args[1];
            seed = (int)args[2];
            minCost = (int)args[3];
            maxCost = (int)args[4];
            numberOfScenarios = (int)args[5];
        }

        public override string ToString()
        {
            return Id;
        }
    }
}
