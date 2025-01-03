using NGAT.Business.Domain.Base;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace NGAT.Business.Domain.Core
{
    public class LinkData : GraphDependantEntity
    {
        public IDictionary<string, string> Attributes
        {
            get
            {
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(this.RawData);
            }
        }

        public string RawData { get; set; }
    }
}
