using NGAT.Business.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NGAT.Business.Contracts.Services.Algorithms
{
    public interface ITurnProhibitionsAssociatedGraph
    {
        Graph OriginalGraph { get; set; }
        Graph DualGraph { get; set; }
        int dualStartNodeId { get; set; }
        int dualEndNodeId { get; set; }
        Graph DualGraphBuilder(Graph OriginalGraph, int startNodeId, int endNodeId);
        List<int> EquivalentPath(List<int> DualPath);
    }
}
