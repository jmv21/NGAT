using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGAT.Business.Domain.Core
{
    public interface ICost
    {
         double cost { get; set; }
         bool IsMultiScenario { get; set; }
         List<double> Costs { get; set; }
    }


    public class Distance : ICost
    {
        private List<double> costs;
        private bool isMultiScenario;
        public double cost { get => Costs[0]; set => Costs[0]= value; }
        public bool IsMultiScenario { get => isMultiScenario; set => isMultiScenario = value; }
        public List<double> Costs { get => costs; set => costs = value; }

        public Distance(List<double> Costs, bool IsMultiScenario)
        {
            this.costs = Costs;
            this.IsMultiScenario = IsMultiScenario;
        }
    }

}
