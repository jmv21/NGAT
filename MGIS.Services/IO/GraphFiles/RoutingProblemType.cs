using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGAT.Services.IO.MapFiles
{
    /// <summary>
    /// Represents the type of routing problem
    /// </summary>
    public enum RoutingProblemType
    {
        
        /// <summary>
        /// Vehicles Routing Problem
        /// </summary>
        VRP = 0,
        /// <summary>
        /// Arcs Routing Problem
        /// </summary>
        ARP = 1,
        /// <summary>
        /// Node Routing Problem
        /// </summary>
        NRP = 2
    }
}
