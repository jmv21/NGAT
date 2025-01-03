using NGAT.Business.Contracts.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NGAT.Services.IO
{
    public class AttributesFetcherCollection : List<Func<IDictionary<string, string>, IEnumerable<KeyValuePair<string, string>>>>, IAttributesFetcherCollection
    {
        public AttributesFetcherCollection()
        {

        }

        public AttributesFetcherCollection(params string[] attributesToFetch)
        {
            Add(attrs => {
                var fetchedAttrs = new List<KeyValuePair<string, string>>();
                foreach (var attr in attributesToFetch)
                {
                    if (attrs.ContainsKey(attr))
                        fetchedAttrs.Add(new KeyValuePair<string, string>(attr, attrs[attr]));
                }
                return fetchedAttrs;
            });
        }

        public IDictionary<string, string> Fetch(IDictionary<string, string> fetchSource)
        {
            Dictionary<string, string> fetched = new Dictionary<string, string>();
            this.ForEach(f =>
            {
                f(fetchSource).ToList().ForEach(kv => fetched.Add(kv.Key, kv.Value));
            });
            return fetched;
        }
    }

}
