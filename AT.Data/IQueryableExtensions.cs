using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Data
{
    /// <summary>
    /// Extension method class for IQueryable
    /// </summary>
    public static class IQueryableExtensions
    {
        /// <summary>
        /// Wraps an IQueryable query as an entity that will be party of a multiple result set.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static MultipleResultQuery<T> AsMultipleResultQuery<T>(this IQueryable<T> query)
        {
            return new MultipleResultQuery<T>(query);
        }
    }
}
