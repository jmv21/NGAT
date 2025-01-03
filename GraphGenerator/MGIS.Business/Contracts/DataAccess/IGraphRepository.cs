using NGAT.Business.Domain.Core;
using System;


namespace NGAT.Business.Contracts.DataAccess
{
    public interface IGraphRepository : IRepository<Graph>
    {
        Graph QuerySubGraph(int graphId, params Tuple<double, double>[] boundingRegion);
        Graph QuerySubGraph(string graphId, params Tuple<double, double>[] boundingRegion);
    }
}
