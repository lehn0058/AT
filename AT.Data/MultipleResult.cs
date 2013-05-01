using AT.Core;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Text;

namespace AT.Data
{
    /// <summary>
    /// Base class for multiple result entites.
    /// </summary>
    public abstract class MultipleResult
    {
        /// <summary>
        /// Uses the given context and reader to map sql query results to a collection of entities of type T.
        /// </summary>
        /// <param name="customDbContext"></param>
        /// <param name="reader"></param>
        public abstract void MapResults(DbContext customDbContext, DbDataReader reader);
    }

    /// <summary>
    /// Used as a map for data returned from a multiple result set.
    /// </summary>
    /// <typeparam name="T">The type of results expected to be returned.</typeparam>
    public class MultipleResult<T> : MultipleResult
    {
        private IEnumerable<T> _results;

        /// <summary>
        /// Convenience method that instantiates the given inResult. Can be used as a shorthand when passing objects along for multiple result sets.
        /// </summary>
        /// <param name="inResult">The entity to Instantiate</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification="Needed for generic instantiation.")]
        public static void Instantiate(MultipleResult inResult)
        {
            Argument.Null(() => inResult);
            inResult = new MultipleResult<T>();
        }

        /// <summary>
        /// Uses the given context and reader to map sql query results to a collection of entities of type T.
        /// </summary>
        /// <param name="customDbContext">The context that is doing the result mapping.</param>
        /// <param name="reader">A data reader with populated results.</param>
        public override void MapResults(DbContext customDbContext, DbDataReader reader)
        {
            Argument.NotNull(() => customDbContext, () => reader);
            _results = customDbContext.Read<T>(reader);
        }

        /// <summary>
        /// Returns the entities retrieved from the wrapped IQueryable that was executed during a multiple result query.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if this property is accessed before it partakes in a multiple result query.</exception>
        public IEnumerable<T> Results
        {
            get
            {
                if (_results == null)
                {
                    throw new InvalidOperationException("Results were attempted to be accessed before the multiple result query was executed.");
                }

                return _results;
            }
        }
    }
}
