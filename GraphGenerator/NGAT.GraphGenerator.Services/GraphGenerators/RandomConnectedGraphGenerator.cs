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
    public class RandomConnectedGraphGenerator : IGraphGenerator
    {
        int nodesAmount;
        int edgesOverNeededAmount;
        int seed;
        double density;
        double maxCost;
        double minCost;
        public int numberOfScenarios { get; set; }

        public string Id { get; set; }
        public string Description { get; set; }

        public RandomConnectedGraphGenerator()
        {
            Id = "Connected Graph Generator";
            Description = "Generate a random connected Graph.\nParameters:\nNodes amount\nDensity\nSeed\nArc Min Cost\nArc Max Cost";
        }
        public Graph Generate()
        {
            Graph newGraph = new Graph();
            Random random = new Random(seed);
            bool [,] addedEdges = new bool[nodesAmount, nodesAmount];
            int availableArcs = (nodesAmount*(nodesAmount-1));
            for (int i = 0; i < nodesAmount; i++)
            {
                newGraph.AddNode(0,0, new Dictionary<string, string>());
            }
            List<int> connectedNodesId = new List<int>() { random.Next(1,nodesAmount + 1) };
            List<int> disconnectedNodesId = new List<int>();

            for (int i = 1; i < nodesAmount+1; i++)
            {
                if (i!=connectedNodesId[0])
                    disconnectedNodesId.Add(i);
            }
            for (int i = 0; i < nodesAmount-1; i++)
            {
                int v1Index = connectedNodesId[random.Next(connectedNodesId.Count)];
                int v2Index = disconnectedNodesId[random.Next(disconnectedNodesId.Count)];

                List<double> costs = new List<double>();
                for (int j = 0; j < numberOfScenarios; j++)
                {
                costs.Add(random.Next((int)minCost, (int)maxCost) + random.NextDouble());
                }
                newGraph.AddLink(v1Index, v2Index, new Distance(costs, costs.Count > 1 ? true : false), new LinkData(), true);
                addedEdges[v1Index-1,v2Index-1] = true;
                availableArcs--;

                costs.Clear();
                for (int j = 0; j < numberOfScenarios; j++)
                {
                    costs.Add(random.Next((int)minCost, (int)maxCost) + random.NextDouble());
                }
                newGraph.AddLink(v2Index, v1Index, new Distance(costs, costs.Count > 1 ? true : false), new LinkData(), true);
                addedEdges[v2Index - 1, v1Index - 1] = true;
                availableArcs--;

                connectedNodesId.Add(v2Index);
                disconnectedNodesId.Remove(v2Index);
            }

            for (int i = 0; i < edgesOverNeededAmount; i++)
            {
                int selected = random.Next(availableArcs);
                bool next = false;
                for (int j = 0; j < nodesAmount; j++)
                {
                    for (int k = 0; k < nodesAmount; k++)
                    {
                        if (j == k)
                            continue;
                        if (!addedEdges[j, k])
                        {
                            if (selected == 0)
                            {
                                List<double> costs = new List<double>();
                                for (int h = 0; h < numberOfScenarios; h++)
                                {
                                    costs.Add(random.Next((int)minCost, (int)maxCost) + random.NextDouble());
                                }
                                newGraph.AddLink(j + 1, k + 1, new Distance(costs, costs.Count > 1 ? true : false), new LinkData(), true);
                                addedEdges[j, k] = true;
                                //addedEdges[j, k] = true;
                                availableArcs--;
                                next = true;
                                break;
                            }
                            else
                                selected--;
                        }
                    }
                    if (next)
                        break;
                }
            }
            seed++;
            return newGraph;
        }

        public IEnumerable<Graph> GenerateAll(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                yield return Generate();
            }
        }

        public void SetParameters(object[] args)
        {
            nodesAmount = (int)args[0];
            double density = (double)args[1];
            if(density<0)
            {
                density = 0;
            }
            else if(density > 1)
            {
                density = 1;
            }
            int edgesNeeded = (int)(((nodesAmount * (nodesAmount - 1))) * density);
            edgesOverNeededAmount = edgesNeeded> (nodesAmount-1)*2? edgesNeeded - (nodesAmount-1)*2 : 0;
            seed = (int)args[2];
            minCost = (int)args[3];
            maxCost = (int)args[4];
            numberOfScenarios = (int)args[5];
        }

        public List<(Type, string)> UsedParameters()
        {
            return new List<(Type, string)> { (typeof(int), "Nodes amount"), (typeof(double), "Density"), (typeof(int), "seed"), (typeof(int), "Min edge cost"), (typeof(int), "Max edge cost"), (typeof(int), "Number of scenarios") };
        }

        public override string ToString()
        {
            return Id;
        }
    }
}
