using NGAT.Business.Domain.Core;
using System;

namespace NGAT.Business.Contracts.Services.Graphs
{
    public interface IGraphsApplicationService : IApplicationService<Graph>
    {
        Graph QuerySubGraph(int graphId, params Tuple<double, double>[] boundingRegion);

        Graph QuerySubGraph(string graphId, params Tuple<double, double>[] boundingRegion);

        Graph FindByName(string graphName);

        Graph GetFullGraph(int graphId);

    }

}
