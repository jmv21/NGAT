using NGAT.Business.Contracts.DataAccess;
using NGAT.Business.Contracts.Services.Graphs;
using NGAT.Business.Domain.Core;
using System;
using System.Linq;

namespace NGAT.Services.ApplicationServices.Graphs
{
    public class GraphsApplicationService : ApplicationService<Graph>, IGraphsApplicationService
    {
        public GraphsApplicationService(IGraphRepository graphRepository) : base(graphRepository)
        {
        }

        protected IGraphRepository GraphsRepository => (IGraphRepository)base.Repository;

        public Graph FindByName(string graphName)
        {
            return Repository.GetAll(g => g.Name.Equals(graphName)).FirstOrDefault();
        }

        public Graph GetFullGraph(int graphId)
        {
            var graph = Repository.GetAll(g => g.Id == graphId, g => g.Nodes, g => g.Edges).FirstOrDefault();
            if (graph != null)
                graph.CompleteBuild();
            return graph;
        }

        public virtual Graph QuerySubGraph(int graphId, params Tuple<double, double>[] boundingRegion)
        {
            return GraphsRepository.QuerySubGraph(graphId, boundingRegion);
        }

        public Graph QuerySubGraph(string graphId, params Tuple<double, double>[] boundingRegion)
        {
            return GraphsRepository.QuerySubGraph(graphId, boundingRegion);
        }
    }

}
