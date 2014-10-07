using System.Data.Entity.Core.Objects;
using AT.Core;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
namespace AT.Data
{
    /// <summary>
    /// Wraps an IQueryable entity for use in a multiple result query.
    /// </summary>
    public abstract class MultipleResultQuery : MultipleResult
    {
        private const String linqParameterFormat = "@p__linq__{0}";
        private const String customParameterFormat = "@p__linq__m__{0}";
        private IQueryable _query;
        private readonly Dictionary<string, object> _parameters = new Dictionary<string, object>();

        /// <summary>
        /// 
        /// </summary>
        protected ObjectQuery ObjectQuery;

        /// <summary>
        /// 
        /// </summary>
        protected MultipleResultMapper Mapper = new MultipleResultMapper();

        /// <summary>
        /// The query that is wrapped by this entity.
        /// </summary>
        protected IQueryable Query
        {
            get { return _query; }
            set { _query = value; }
        }

        /// <summary>
        /// Retrieves the SQL query the wrapped IQueryable represents. Adjusts the parameter names based on the index passed in.
        /// Passing in 0 will result in no adjustment.
        /// </summary>
        /// <param name="parameterIndexStart">The index to start labeling parameters at.</param>
        /// <returns>A query with adjusted indexes produced by the wrapped IQueryable.</returns>
        internal String RetrieveSqlQuery(int parameterIndexStart)
        {
            _parameters.Clear();

            FieldInfo fi = _query.GetType().GetField("_internalQuery", BindingFlags.NonPublic | BindingFlags.Instance);
            object q2 = fi.GetValue(_query);

            PropertyInfo pi = q2.GetType().GetProperty("ObjectQuery", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            //get query
            ObjectQuery = (ObjectQuery)pi.GetValue(q2, null);
            string formatedQuery = ObjectQuery.ToTraceString();


            int currentParameterIndex = parameterIndexStart;
            for (int i = 0; i < ObjectQuery.Parameters.Count; i++)
            {
                String parameterString = String.Format(CultureInfo.InvariantCulture, linqParameterFormat, i);
                String newParameterString = String.Format(CultureInfo.InvariantCulture, customParameterFormat, currentParameterIndex);
                formatedQuery = formatedQuery.Replace(parameterString, newParameterString);
                _parameters[newParameterString] = ObjectQuery.Parameters[parameterString.Substring(1)].Value;
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
            foreach (KeyValuePair<string, object> parameter in _parameters)
            {
                sqlParameters.Add(new SqlParameter(parameter.Key, parameter.Value));
                parameterIndex++;
            }
            return parameterIndex;
        }
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
        }

        /// <summary>
        /// Uses the given context and reader to map sql query results to a collection of entities of type T.
        /// </summary>
        /// <param name="customDbContext"></param>
        /// <param name="reader"></param>
        public override void MapResults(DbContext customDbContext, DbDataReader reader)
        {
            Argument.NotNull(() => customDbContext, () => reader);

            DbDataReader reader2 = Mapper.WrapDataReader(ObjectQuery, reader);
            _results = customDbContext.Read<T>(reader2);
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