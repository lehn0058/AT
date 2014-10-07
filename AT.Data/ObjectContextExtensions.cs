using System.Data.Entity.Core.Objects;
using System.Linq;
using AT.Core;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace AT.Data
{
    /// <summary>
    /// Extension methods for the System.Data.Objects.ObjectContext class.
    /// </summary>
    public static class ObjectContextExtensions
    {
        /// <summary>
        /// Compact translation of reader results into an object.
        /// </summary>
        /// <typeparam name="T">The type of entities we are expecting the reader to return.</typeparam>
        /// <param name="objectContext">The object context we are extending.</param>
        /// <param name="reader">A populated data reader popualted with database results.</param>
        /// <returns>A collection of mapped entities that were read from the data reader.</returns>
        [Obsolete("Use DbContextExtensions.Read instead. This maps the read entities into the context correctly.")]
        public static IEnumerable<T> Read<T>(this ObjectContext objectContext, DbDataReader reader)
        {
            Argument.NotNull(() => objectContext, () => reader);

            List<T> result = objectContext.Translate<T>(reader).ToList();
            reader.NextResult();
            return result;
        }
    }
}
