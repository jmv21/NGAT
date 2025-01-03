using System;
using System.IO;
using GMap.NET;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using NGAT.Business.Contracts.Services.DataPointsImportFormat;
using System.Windows.Forms;

namespace NGAT.Services.DataImporters
{
    public class DataImportRPD : IDataPointsImporter
    {

        public string Filter => "Files RPD (*.rpd)|*rpd";

        public (List<PointLatLng>, List<string>) Import(string path, Form owner)
        {
            List<string> nodesData = new List<string>();
            List<PointLatLng> inputPoints = new List<PointLatLng>();
            using (FileStream rpd = new FileStream(path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(rpd))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var data = line.Split(';');
                        nodesData.Add(data[0]);
                        double lat = double.Parse(data[1].Replace(',', '.'), CultureInfo.InvariantCulture);
                        double lng = double.Parse(data[2].Replace(',', '.'), CultureInfo.InvariantCulture);
                        inputPoints.Add(new PointLatLng(lat, lng));
                    }

                }
            }
            return (inputPoints, nodesData);
        }

        public override string ToString()
        {
            return "RPD";
        }

    }
}
