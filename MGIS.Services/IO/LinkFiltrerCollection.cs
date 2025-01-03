using NGAT.Business.Contracts.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NGAT.Services.IO
{
    /// <summary>
    /// The default implementation for a road network link filter
    /// </summary>
    public class LinkFiltrerCollection : List<Func<IDictionary<string, string>, bool>>, IAttributeFilterCollection
    {

        public bool ApplyAllFilters(IDictionary<string, string> attributes)
        {
            return this.All(f => f(attributes));
        }
    }

}
