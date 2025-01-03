using NGAT.Business.Domain.Core;
using NGAT.Business.Contracts.Services.ForbiddenTurns;
using System.Linq;

namespace NGAT.Services.ForbiddenTurns
{
    /// <summary>
    /// Represents the classic way to determine when is a left turn forbidden.
    /// </summary>
    public class ClassicForbiddenTurnFilter : IForbiddenTurnFilter
    {
        /// <summary>
        /// Returns True if <node1, node2, node3> is a left turn and <node1, node2> is an edge and <node2, node3> is an arc.
        /// </summary>
        /// <param name="node1">Source node of the turn.</param>
        /// <param name="node2">Middle node of the turn</param>
        /// <param name="node3">Destiny node of the turn.</param>
        /// <returns></returns>
        public bool IsForbiddenTurn(Node node1, Node node2, Node node3)
        {
            if (node1 == null ) return false;
            if (!(node1.Edges.Where(e => e.ToNode == node2 || e.FromNode == node2).Any() 
                && node2.OutgoingArcs.Where(a => a.ToNode == node3).Any()))
                return false;

            double v1X = node2.Coordinate[1] - node1.Coordinate[1];
            double v1Y = node2.Coordinate[0] - node1.Coordinate[0];
            double v2X = node3.Coordinate[1] - node2.Coordinate[1];
            double v2Y = node3.Coordinate[0] - node2.Coordinate[0];
            
            return v1X * v2Y > v1Y * v2X;
        }
    }
}
