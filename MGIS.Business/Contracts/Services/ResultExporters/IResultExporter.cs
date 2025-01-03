using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET.WindowsForms;
using NGAT.Business.Domain.Core;

namespace NGAT.Business.Contracts.Services.ResultExporters
{
    public interface IResultExporter
    {
        void Export(Graph graph, ShortestPathProblemOutput result, GMapControl gmap);
    }
}