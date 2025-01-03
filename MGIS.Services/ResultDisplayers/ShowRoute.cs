using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGAT.Business.Contracts.Services.ResultDisplayers;
using GMap.NET.WindowsForms;
using GMap.NET;
using System.Drawing;
using GMap.NET.WindowsForms.Markers;
using NGAT.Business.Domain.Core;

namespace NGAT.Services.ResultDisplayers
{
    public class ShowRoute : IResultDisplayer
    {
        public void Display(ShortestPathProblemOutput result, GMapControl gmap)
        {
            List<PointLatLng> points = new List<PointLatLng>();

            GMapOverlay resultOverlay = gmap.Overlays.FirstOrDefault(o => o.Id == "result");
            if (resultOverlay == null)
            { resultOverlay = new GMapOverlay("result"); }
            resultOverlay.Clear();

            foreach (dynamic item in result.Points)
            {
                points.Add(new PointLatLng(item.Latitude, item.Longitude));
            }
            GMarkerGoogle startMarker = new GMarkerGoogle(new PointLatLng(result.startPoint.Latitude, result.startPoint.Longitude) , GMarkerGoogleType.blue_dot);
            startMarker.ToolTipText = "Start";
            resultOverlay.Markers.Add(startMarker);
            int meters = (int)(result.Distance);
            float Km = (float)meters / 1000;
            string s = (result.Distance <= 1000) ? meters + " m)" : Km + " km)";
            if (meters<=0)
            {
                s = "";
            }
            GMarkerGoogle endMarker = new GMarkerGoogle(new PointLatLng(result.endPoint.Latitude, result.endPoint.Longitude), GMarkerGoogleType.blue_dot);
            endMarker.ToolTipText = "End(" + s;
            resultOverlay.Markers.Add(endMarker);
            GMapRoute route = new GMapRoute(points, "Route");
            resultOverlay.Routes.Add(route);


            //Export markers
            for (int i = 0; i < points.Count; i++)
            {
                string extra_marker = "";
                for (int j = i - 1; j >= 0; j--)
                {
                    if (points[i] == points[j])
                        extra_marker = (j+1).ToString() + " ";
                }
                GMapMarker mark = new CustomMarker(points[i], new Bitmap("Resources\\BitMaps\\blue_point.png"), extra_marker.Length > 0 ? extra_marker + (i + 1).ToString() : (i + 1).ToString());
                resultOverlay.Markers.Add(mark);
                gmap.Overlays.Add(resultOverlay);
                gmap.Refresh();
                gmap.Zoom += 1;
                gmap.Zoom -= 1;
            }
        }

        public override string ToString()
        {
            return "Show Route";
        }
    }
}
