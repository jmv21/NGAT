using System;
using System.Collections.Generic;

namespace NGAT.Business.Contracts.IO
{
    /// <summary>
    /// Represents a collection of node filters
    /// </summary>
    public interface IAttributeFilterCollection : IList<Func<IDictionary<string, string>, bool>>
    {
        /// <summary>
        /// Applies all filters registered in this collection to the specified <paramref name="attributes"/>
        /// </summary>
        /// <param name="attributes">The attributes to filter for</param>
        /// <returns>True if <paramref name="attributes"/> passes all filters registered in this collection.</returns>
        bool ApplyAllFilters(IDictionary<string, string> attributes);
    }

}
