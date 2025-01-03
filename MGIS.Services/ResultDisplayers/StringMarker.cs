using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET.WindowsForms.Markers;
using GMap.NET;
using System.Drawing;

namespace NGAT.Services.ResultDisplayers
{
    public class CustomMarker : GMarkerGoogle
    {
        public CustomMarker(PointLatLng p, Bitmap Bitmap, string text) : base(p, Bitmap)
        {
            this.text = text;
        }

        public string text { get; set; }

        public override void OnRender(Graphics g)
        {
            base.OnRender(g);
            g.DrawString(text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, base.LocalPosition.X, base.LocalPosition.Y);
        }

    }
}
