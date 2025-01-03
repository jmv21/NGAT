using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET.WindowsForms;

namespace NGAT.Business.Contracts.Services.ResultDisplayers
{
    public interface IResultDisplayer
    {
         void Display(dynamic result, GMapControl gmap);
    }
}
