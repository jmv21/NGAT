using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGAT.Business.Domain.Core;

namespace NGAT.GraphGenerator.Business.Contracts
{
    public interface IGraphGenerator
    {
        public Graph Generate();
        public IEnumerable<Graph> GenerateAll(int amount);
        public string Id { get; set; }
        public string Description { get; set; }
        public List<(Type, string)> UsedParameters();
        public void SetParameters(object[] args);

    }
}
