using System;
using System.Collections.Generic;
using GMap.NET;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NGAT.Business.Contracts.Services.DataPointsImportFormat
{
    public interface IDataPointsImporter
    {
        string Filter { get; }
        (List<PointLatLng>, List<string>) Import(string path, Form owner);

    }

}

