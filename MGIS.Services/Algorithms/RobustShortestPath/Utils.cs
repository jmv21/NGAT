using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGAT.Business.Domain.Core;

namespace NGAT.Services.Algorithms.RobustShortestPath
{
    public class Utils
    {
        /// <summary>
        /// Calculate all the simple pats from start point to end point
        /// </summary>
        /// <param name="costs"></param>
        /// <param name="graph"></param>
        /// <param name="sp"></param>
        /// <param name="ep"></param>
        /// <returns></returns>
        public static List<(List<int>,List<double>)>CalculatePaths(double[][][] costs, int sp, int ep)
        {
            bool [] visited = new bool[costs[0].Length];
            visited[sp] = true;
            List<List<int>> paths = Paths(new List<int> {sp}, ep, costs[0].Length, visited, costs);
            List<(List<int>, List<double>)> ps = new List<(List<int>, List<double>)>();

            foreach (var path in paths)
            {
                List<double> cost = new List<double>();
                for (int i = 0; i < costs.Length; i++)
                {
                    cost.Add(0);
                }
                for (int i = 0; i < path.Count-1; i++)
                {
                    for (int j = 0; j < cost.Count; j++)
                    {
                        cost[j]+=costs[j][path[i]][path[i+1]];
                    }
                }
                ps.Add((path,cost));
            }
            return ps;
        }


        static List<List<int>> Paths ( List<int> actualPath, int ep, int maxLength, bool [] visited, double[][][] costs)
        {
            if (actualPath[actualPath.Count-1]==ep)
            {
                return new List<List<int>>{actualPath};
            }
            else if (actualPath.Count==maxLength)
                return new List<List<int>> { };

            List <List<int>> paths = new List<List<int>>();
            List<int> adjacents = new List<int>();
            for (int i = 0; i < costs[0].Length; i++)
            {
                if (costs[0][actualPath[actualPath.Count-1]][i]!=-1)
                    adjacents.Add(i);
            }

            foreach (int adjacent in adjacents)
            {
                if (!visited[adjacent])
                {
                    bool[] newVisited = new bool[visited.Length];
                    visited.CopyTo(newVisited, 0);
                    newVisited[adjacent] = true;
                    List<int> newActualPath = new List<int>();
                    foreach (int node in actualPath)
                    {
                        newActualPath.Add(node);
                    }
                    newActualPath.Add(adjacent);
                    List<List<int>> news = Paths(newActualPath, ep, maxLength, newVisited, costs);
                    foreach (List<int> path in news)
                    {
                        paths.Add(path);
                    }
                }
            }


            //foreach (Arc arc in actualPath[actualPath.Count-1].OutgoingArcs)
            //{
            //    if (!visited[arc.ToNodeId])
            //    {
            //        bool [] newVisited = new bool[visited.Length];
            //        visited.CopyTo(newVisited, 0);
            //        newVisited[arc.ToNodeId-1] = true;
            //        List<Node> newActualPath = new List<Node>();
            //        foreach (Node node in actualPath)
            //        {
            //            newActualPath.Add(node);
            //        }
            //        newActualPath.Add(arc.ToNode);
            //        paths.Concat(Paths(newActualPath, ep, maxLength, newVisited));
            //    }
            //}

            //foreach (Edge edge in actualPath[actualPath.Count - 1].Edges)
            //{
            //    Node nextNode = actualPath[actualPath.Count-1]== edge.ToNode? edge.FromNode : edge.ToNode;
            //    if (!visited[nextNode.Id-1])
            //    {
            //        bool[] newVisited = new bool[visited.Length];
            //        visited.CopyTo(newVisited, 0);
            //        newVisited[nextNode.Id-1] = true;
            //        List<Node> newActualPath = new List<Node>();
            //        foreach (Node node in actualPath)
            //        {
            //            newActualPath.Add(node);
            //        }
            //        newActualPath.Add(nextNode);
            //        paths.Concat(Paths(newActualPath, ep, maxLength, newVisited));
            //    }
            //}
            return paths;
        }
    }
}
