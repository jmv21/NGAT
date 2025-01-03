using GMap.NET.WindowsForms;
using NGAT.Services.IO.MapFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGAT.Visual.RoutesDrawing
{
    public interface IRouteDrawer
    {
        RoutingProblemType ProblemType { get; }
        void DrawRoute(GMapOverlay gMapOverlay, List<int> nodes);
    }

    public class ARPStaticRouteDrawer : IRouteDrawer
    {
        public RoutingProblemType ProblemType => RoutingProblemType.ARP;

        public void DrawRoute(GMapOverlay gMapOverlay, List<int> nodes)
        {
            throw new NotImplementedException();
        }
    }
}
