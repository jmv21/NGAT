using NGAT.Business.Domain.Core;

namespace NGAT.Business.Contracts.Services.ForbiddenTurns
{
    public interface IForbiddenTurnFilter
    {
        /// <summary>
        /// Returns True if <node1, node2, node3> is a forbidden turn.
        /// </summary>
        /// <param name="node1">Source node of the turn.</param>
        /// <param name="node2">Middle node of the turn</param>
        /// <param name="node3">Destiny node of the turn.</param>
        /// <returns></returns>
        bool IsForbiddenTurn(Node node1, Node node2, Node node3);
    }
}
