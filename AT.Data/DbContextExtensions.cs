using AT.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AT.Data
{
    /// <summary>
    /// Extensions to the Entity Framework DbContext class.
    /// </summary>
    public static class DbContextExtensions
    {
        private const String parameterFormat = "@p__linq__{0}";

        /// <summary>
        /// Extends the DbContext class to be able to return multiple results sets.
        /// This is done using only 1 round trip to the database.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="db"></param>
        /// <param name="query1"></param>
        /// <param name="query2"></param>
        /// <returns></returns>
        public static Tuple<IEnumerable<T1>, IEnumerable<T2>> MultipleResultSet<T1, T2>(this DbContext db, IQueryable<T1> query1, IQueryable<T2> query2)
        {
            Argument.NotNull(() => query1, () => query2);
            List<IQueryable> queries = new List<IQueryable> { query1, query2 };
            return MultipleResultSetInternal(db, queries, (objectContext, reader) => objectContext.Read<T1, T2>(reader));
        }

        /// <summary>
        /// Extends the DbContext class to be able to return multiple results sets.
        /// This is done using only 1 round trip to the database.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="db"></param>
        /// <param name="query1"></param>
        /// <param name="query2"></param>
        /// <param name="query3"></param>
        /// <returns></returns>
        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> MultipleResultSet<T1, T2, T3>(this DbContext db, IQueryable<T1> query1, IQueryable<T2> query2, IQueryable<T3> query3)
        {
            Argument.NotNull(() => query1, () => query2, () => query3);
            List<IQueryable> queries = new List<IQueryable> { query1, query2, query3 };
            return MultipleResultSetInternal(db, queries, (objectContext, reader) => objectContext.Read<T1, T2, T3>(reader));
        }

        /// <summary>
        /// Extends the DbContext class to be able to return multiple results sets.
        /// This is done using only 1 round trip to the database.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <param name="db"></param>
        /// <param name="query1"></param>
        /// <param name="query2"></param>
        /// <param name="query3"></param>
        /// <param name="query4"></param>
        /// <returns></returns>
        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>> MultipleResultSet<T1, T2, T3, T4>(this DbContext db, IQueryable<T1> query1, IQueryable<T2> query2, IQueryable<T3> query3, IQueryable<T4> query4)
        {
            Argument.NotNull(() => query1, () => query2, () => query3, () => query4);
            List<IQueryable> queries = new List<IQueryable> { query1, query2, query3, query4 };
            return MultipleResultSetInternal(db, queries, (objectContext, reader) => objectContext.Read<T1, T2, T3, T4>(reader));
        }

        /// <summary>
        /// Extends the DbContext class to be able to return multiple results sets.
        /// This is done using only 1 round trip to the database.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <param name="db"></param>
        /// <param name="query1"></param>
        /// <param name="query2"></param>
        /// <param name="query3"></param>
        /// <param name="query4"></param>
        /// <param name="query5"></param>
        /// <returns></returns>
        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>> MultipleResultSet<T1, T2, T3, T4, T5>(this DbContext db, IQueryable<T1> query1, IQueryable<T2> query2, IQueryable<T3> query3, IQueryable<T4> query4, IQueryable<T5> query5)
        {
            Argument.NotNull(() => query1, () => query2, () => query3, () => query4, () => query5);
            List<IQueryable> queries = new List<IQueryable> { query1, query2, query3, query4, query5 };
            return MultipleResultSetInternal(db, queries, (objectContext, reader) => objectContext.Read<T1, T2, T3, T4, T5>(reader));
        }

        /// <summary>
        /// Extends the DbContext class to be able to return multiple results sets.
        /// This is done using only 1 round trip to the database.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <param name="db"></param>
        /// <param name="query1"></param>
        /// <param name="query2"></param>
        /// <param name="query3"></param>
        /// <param name="query4"></param>
        /// <param name="query5"></param>
        /// <param name="query6"></param>
        /// <returns></returns>
        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>> MultipleResultSet<T1, T2, T3, T4, T5, T6>(this DbContext db, IQueryable<T1> query1, IQueryable<T2> query2, IQueryable<T3> query3, IQueryable<T4> query4, IQueryable<T5> query5, IQueryable<T6> query6)
        {
            Argument.NotNull(() => query1, () => query2, () => query3, () => query4, () => query5, () => query6);
            List<IQueryable> queries = new List<IQueryable> { query1, query2, query3, query4, query5, query6 };
            return MultipleResultSetInternal(db, queries, (objectContext, reader) => objectContext.Read<T1, T2, T3, T4, T5, T6>(reader));
        }

        /// <summary>
        /// Extends the DbContext class to be able to return multiple results sets.
        /// This is done using only 1 round trip to the database.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <param name="db"></param>
        /// <param name="query1"></param>
        /// <param name="query2"></param>
        /// <param name="query3"></param>
        /// <param name="query4"></param>
        /// <param name="query5"></param>
        /// <param name="query6"></param>
        /// <param name="query7"></param>
        /// <returns></returns>
        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>> MultipleResultSet<T1, T2, T3, T4, T5, T6, T7>(this DbContext db, IQueryable<T1> query1, IQueryable<T2> query2, IQueryable<T3> query3, IQueryable<T4> query4, IQueryable<T5> query5, IQueryable<T6> query6, IQueryable<T7> query7)
        {
            Argument.NotNull(() => query1, () => query2, () => query3, () => query4, () => query5, () => query6, () => query7);
            List<IQueryable> queries = new List<IQueryable> { query1, query2, query3, query4, query5, query6, query7 };
            return MultipleResultSetInternal(db, queries, (objectContext, reader) => objectContext.Read<T1, T2, T3, T4, T5, T6, T7>(reader));
        }

        /// <summary>
        /// Builds a single SqlCommand from the given IQueryable queries.
        /// </summary>
        /// <param name="queries">The queries to combine into a single SQL command.</param>
        /// <returns>A single, combined SQL command that returns multiple results.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification="Query is generated from compiled code and is still parameterized"),
        System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Up to the called of this method to call dispose.")]
        public static SqlCommand BuildSqlCommand(IEnumerable<IQueryable> queries)
        {
            Argument.NotNull(() => queries);

            StringBuilder bulkSQLOperation = new StringBuilder();
            int parameterIndex = 0;
            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            // For each query that was passed in, visit each node in its expression tree.
            // Each time we find a parameter, keep track of that parameter so we can build
            // a collection of sql parameters to pass in with the queries.
            ParameterExtractorExpressionVisitor visitor = new ParameterExtractorExpressionVisitor();
            foreach (IQueryable query in queries)
            {
                visitor.Visit(query);
                string queryString = QueryParameterCleanup(parameterIndex, query, visitor);
                parameterIndex = BuildParameters(query, visitor, parameterIndex, sqlParameters);
                bulkSQLOperation.Append(queryString);
                bulkSQLOperation.Append(Environment.NewLine);
                bulkSQLOperation.Append(Environment.NewLine);
            }

            SqlCommand command = new SqlCommand();
            command.CommandText = bulkSQLOperation.ToString();
            command.CommandType = CommandType.Text;
            command.Parameters.AddRange(sqlParameters.ToArray());

            return command;
        }

        /// <summary>
        /// Builds and executes a multiple result set query. Informs the given translateFunction
        /// to transform the results into mapped entities.
        /// </summary>
        /// <typeparam name="T">The type the results have been mapped to.</typeparam>
        /// <param name="db">The context being extended.</param>
        /// <param name="queries">A collection of queries that is to be executed.</param>
        /// <param name="translateFunction">A function that is able to take the results of
        /// executing a multiple result set query and map those results to entities.</param>
        /// <returns>The mapped result set entities.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification="SQL query is parameterized")]
        private static T MultipleResultSetInternal<T>(
            this DbContext db,
            IEnumerable<IQueryable> queries,
            Func<ObjectContext, DbDataReader, T> translateFunction)
        {
            // Execute the parameterized SQL command which will return multiple result sets
            ObjectContext objectContext = ((IObjectContextAdapter)db).ObjectContext;
            DbConnection storeConnection = ((EntityConnection)objectContext.Connection).StoreConnection;

            try
            {
                using (DbCommand command = BuildSqlCommand(queries))
                {
                    command.Connection = storeConnection;
                    storeConnection.Open();

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        return translateFunction.Invoke(objectContext, reader);
                    }
                }
            }
            finally
            {
                storeConnection.Close();
            }
        }

        private static string QueryParameterCleanup(int parameterIndexStart, IQueryable query, ParameterExtractorExpressionVisitor visitor)
        {
            string formatedQuery = query.ToString(); 
            int currentParameterIndex = parameterIndexStart;
            for (int i = 0; i < visitor.ExtractedParametersForQuery(query).Count; i++)
            {
                String parameterString = String.Format(CultureInfo.InvariantCulture, parameterFormat, i);
                String newParameterString = String.Format(CultureInfo.InvariantCulture, parameterFormat, currentParameterIndex);
                formatedQuery = formatedQuery.Replace(parameterString, newParameterString);
                currentParameterIndex++;
            }

            return formatedQuery;
        }

        private static int BuildParameters(IQueryable query, ParameterExtractorExpressionVisitor visitor, int parameterIndex, List<SqlParameter> sqlParameters)
        {
            foreach (object parameter in visitor.ExtractedParametersForQuery(query))
            {
                String parameterName = String.Format(CultureInfo.InvariantCulture, parameterFormat, parameterIndex);
                sqlParameters.Add(new SqlParameter(parameterName, parameter));
                parameterIndex++;
            }
            return parameterIndex;
        }
    }
}
