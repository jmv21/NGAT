using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGAT.Business.Domain.Core
{

    public class ShortestPathProblemOutput
    {
        /// <summary>
        /// The distance between start and end points.
        /// </summary>
        public double Distance { get; private set; }

        /// <summary>
        /// A list with the coordinates of the road's points.
        /// </summary>
        public dynamic Points { get; private set; }

        /// <summary>
        /// The input start point
        /// </summary>
        public dynamic startPoint { get; set; }

        /// <summary>
        /// The input end point.
        /// </summary>
        public dynamic endPoint { get; set; }

        /// <summary>
        /// An IEnumerable with the Id of each visited node in the path.
        /// </summary>
        public IEnumerable<int> NodesId { get; private set; }



        public ShortestPathProblemOutput(double distance, IEnumerable<int> nodesId, dynamic points, dynamic start, dynamic end)
        {
            startPoint = start;
            endPoint = end;
            Distance = distance;
            NodesId = nodesId;
            Points = points;
        }
    }
}
