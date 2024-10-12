using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pooshit.AspNetCore.Services.Data;

namespace Pooshit.AspNetCore.Services.Extensions {

    /// <summary>
    /// extensions for page methods
    /// </summary>
    public static class PageExtensions {

        /// <summary>
        /// lists all data provided by a list method
        /// </summary>
        /// <typeparam name="TData">type of data to retrieve</typeparam>
        /// <typeparam name="TFilter">type of filter to provide to list method</typeparam>
        /// <param name="filter">filter to provide to list method</param>
        /// <param name="listmethod">list method to call</param>
        /// <returns>all data provided by list method</returns>
        public static IEnumerable<TData> ListAll<TData, TFilter>(this Func<TFilter, Task<Page<TData>>> listmethod, TFilter filter=null)
        where TFilter : ListFilter, new() {
            filter ??= new TFilter();
            do {
                Task<Page<TData>> listtask = listmethod(filter);
                Page<TData> result = listtask.ConfigureAwait(false).GetAwaiter().GetResult();
                foreach (TData data in result.Result)
                    yield return data;
                filter.Continue = result.Continue;
            } while (filter.Continue.HasValue);
        }
    }
}