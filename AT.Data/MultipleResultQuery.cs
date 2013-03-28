using AT.Core;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Data
{
    /// <summary>
    /// Wraps an IQueryable entity for use in a multiple result query.
    /// </summary>
    public abstract class MultipleResultQuery
    {
        private const String parameterFormat = "@p__linq__{0}";
        private IQueryable _query;
        private ParameterExtractorExpressionVisitor _visitor = new ParameterExtractorExpressionVisitor();

        /// <summary>
        /// The query that is wrapped by this entity.
        /// </summary>
        protected IQueryable Query
        {
            get { return _query; }
            set { _query = value; }
        }
        
        /// <summary>
        /// Expression visitor capable of extracting parameters from an IQueryable.
        /// </summary>
        protected ParameterExtractorExpressionVisitor Visitor
        {
            get { return _visitor; }
        }

        /// <summary>
        /// Retrieves the SQL query the wrapped IQueryable represents. Adjusts the parameter names based on the index passed in.
        /// Passing in 0 will result in no adjustment.
        /// </summary>
        /// <param name="parameterIndexStart">The index to start labeling parameters at.</param>
        /// <returns>A query with adjusted indexes produced by the wrapped IQueryable.</returns>
        internal String RetrieveSqlQuery(int parameterIndexStart)
        {
            string formatedQuery = _query.ToString();
            int currentParameterIndex = parameterIndexStart;
            for (int i = 0; i < _visitor.ExtractedParametersForQuery(_query).Count; i++)
            {
                String parameterString = String.Format(CultureInfo.InvariantCulture, parameterFormat, i);
                String newParameterString = String.Format(CultureInfo.InvariantCulture, parameterFormat, currentParameterIndex);
                formatedQuery = formatedQuery.Replace(parameterString, newParameterString);
                currentParameterIndex++;
            }

            return formatedQuery;
        }

        /// <summary>
        /// Builds a collection of sql parameters using the given index as a starting point for the parameter names.
        /// </summary>
        /// <param name="parameterIndex">The index for the parameter we last left off at.</param>
        /// <param name="sqlParameters">A collection to add our newly created sql parameters to.</param>
        /// <returns>The new parameter index we left off at.</returns>
        internal int BuildParameters(int parameterIndex, List<SqlParameter> sqlParameters)
        {
            foreach (object parameter in _visitor.ExtractedParametersForQuery(_query))
            {
                String parameterName = String.Format(CultureInfo.InvariantCulture, parameterFormat, parameterIndex);
                sqlParameters.Add(new SqlParameter(parameterName, parameter));
                parameterIndex++;
            }
            return parameterIndex;
        }

        /// <summary>
        /// Uses the given context and reader to map sql query results to a collection of entities of type T.
        /// </summary>
        /// <param name="objectContext"></param>
        /// <param name="reader"></param>
        internal abstract void MapResults(ObjectContext objectContext, DbDataReader reader);
    }

    /// <summary>
    /// Wraps an IQueryable entity for use in a multiple result query.
    /// </summary>
    /// <typeparam name="T">The return type of the IQueryable that is wrapped inside this entity.</typeparam>
    public class MultipleResultQuery<T> : MultipleResultQuery
    {
        private IEnumerable<T> _results;

        /// <summary>
        /// Wraps the given IQueryable entity for use in multiple result queries.
        /// </summary>
        /// <param name="query">The query to wrap.</param>
        public MultipleResultQuery(IQueryable<T> query)
        {
            Query = Argument.NotNull(() => query);
            Visitor.Visit(Query);
        }

        /// <summary>
        /// Uses the given context and reader to map sql query results to a collection of entities of type T.
        /// </summary>
        /// <param name="objectContext"></param>
        /// <param name="reader"></param>
        internal override void MapResults(ObjectContext objectContext, DbDataReader reader)
        {
             _results = objectContext.Read<T>(reader);
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
