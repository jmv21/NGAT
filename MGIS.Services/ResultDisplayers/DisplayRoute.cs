using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET.WindowsForms;
using NGAT.Business.Contracts.Services.ResultDisplayers;
using GMap.NET;
using System.Threading;
using GMap.NET.WindowsForms.Markers;
using System.Drawing;
using NGAT.Business.Domain.Core;

namespace NGAT.Services.ResultDisplayers
{
    public class DisplayRoute : IResultDisplayer
    {
        GMapOverlay resultOverlay;
        List<PointLatLng> points = new List<PointLatLng>();
        GMapControl gmap;
        double endLat;
        double endLon;

        public void Display(ShortestPathProblemOutput result, GMapControl Gmap)
        {

            gmap = Gmap;

            resultOverlay = gmap.Overlays.FirstOrDefault(o => o.Id == "result");
            if (resultOverlay == null)
            { resultOverlay = new GMapOverlay("result"); }
            resultOverlay.Clear();

            gmap.Overlays.Add(resultOverlay);
            endLat = result.endPoint.Latitude;
            endLon = result.endPoint.Longitude;

            points.Clear();

            foreach (dynamic item in result.Points)
            {
                PointLatLng point = new PointLatLng(item.Latitude, item.Longitude);
                points.Add(point);
                //GMapMarker marker = new GMarkerGoogle(point, new Bitmap("Resources\\BitMaps\\small_point.png"));
                //gmap.Overlays[gmap.Overlays.Count - 1].Markers.Add(marker);
            }
            GMarkerGoogle startMarker = new GMarkerGoogle(new PointLatLng(result.startPoint.Latitude, result.startPoint.Longitude), GMarkerGoogleType.blue_dot);
            startMarker.ToolTipText = "Start";
            resultOverlay.Markers.Add(startMarker);

            int meters = (int)(result.Distance);
            float Km = (float)meters / 1000;
            string s = (result.Distance <= 1000) ? meters + " m)" : Km + " km)";
            if (meters<=0)
            {
                s = "";
            }


            Thread thread = new Thread(AsyncDrawM);
            thread.Start(s);

        }

        void AsyncDrawM(object s)
        {

            var tmp = new List<PointLatLng> { points[0] };
            var route = new GMapRoute(tmp, "path");
            resultOverlay.Routes.Add(route);
            GMapMarker car = new GMarkerGoogle(points[0], new Bitmap("Resources\\BitMaps\\purple_car.png"));
            resultOverlay.Markers.Add(car);
            for (int i = 1; i < points.Count; i++)
            {
                Thread.Sleep(300);
                tmp.Add(points[i]);
                resultOverlay.Routes.Clear();
                route = new GMapRoute(tmp, "path");
                resultOverlay.Routes.Add(route);
                resultOverlay.Markers.Remove(car);
                car = new GMarkerGoogle(points[i], new Bitmap("Resources\\BitMaps\\purple_car.png"));
                resultOverlay.Markers.Add(car);
            }
            GMarkerGoogle endMarker = new GMarkerGoogle(new PointLatLng(endLat, endLon), GMarkerGoogleType.blue_dot);
            endMarker.ToolTipText = "End(" + s;
            resultOverlay.Markers.Add(endMarker);
        }

        public override string ToString()
        {
            return "Display Route";
        }
    }
}
