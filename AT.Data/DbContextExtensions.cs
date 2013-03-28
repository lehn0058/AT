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
        /// <summary>
        /// Capable of executing N number of Entity Framework IQueryable entities that have been wrapped inside a MultipleResult.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="queries"></param>
        public static void MultipleResultSet(this DbContext db, params MultipleResultQuery[] queries)
        {
            Argument.NotNull(() => queries);
            MultipleResultSetInternal(db, queries);
        }

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
        [Obsolete("Use the MultipleResultSet overload that takes in MultiResult")]
        public static Tuple<IEnumerable<T1>, IEnumerable<T2>> MultipleResultSet<T1, T2>(this DbContext db, IQueryable<T1> query1, IQueryable<T2> query2)
        {
            Argument.NotNull(() => query1, () => query2);

            var multiQuery1 = query1.AsMultipleResultQuery();
            var multiQuery2 = query2.AsMultipleResultQuery();

            List<MultipleResultQuery> queries = new List<MultipleResultQuery> { multiQuery1, multiQuery2 };
            MultipleResultSetInternal(db, queries);
            return Tuple.Create(multiQuery1.Results, multiQuery2.Results);
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
        [Obsolete("Use the MultipleResultSet overload that takes in MultiResult")]
        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> MultipleResultSet<T1, T2, T3>(this DbContext db, IQueryable<T1> query1, IQueryable<T2> query2, IQueryable<T3> query3)
        {
            Argument.NotNull(() => query1, () => query2, () => query3);

            var multiQuery1 = query1.AsMultipleResultQuery();
            var multiQuery2 = query2.AsMultipleResultQuery();
            var multiQuery3 = query3.AsMultipleResultQuery();

            List<MultipleResultQuery> queries = new List<MultipleResultQuery> { multiQuery1, multiQuery2, multiQuery3 };
            MultipleResultSetInternal(db, queries);
            return Tuple.Create(multiQuery1.Results, multiQuery2.Results, multiQuery3.Results);
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
        [Obsolete("Use the MultipleResultSet overload that takes in MultiResult")]
        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>> MultipleResultSet<T1, T2, T3, T4>(this DbContext db, IQueryable<T1> query1, IQueryable<T2> query2, IQueryable<T3> query3, IQueryable<T4> query4)
        {
            Argument.NotNull(() => query1, () => query2, () => query3, () => query4);

            var multiQuery1 = query1.AsMultipleResultQuery();
            var multiQuery2 = query2.AsMultipleResultQuery();
            var multiQuery3 = query3.AsMultipleResultQuery();
            var multiQuery4 = query4.AsMultipleResultQuery();

            List<MultipleResultQuery> queries = new List<MultipleResultQuery> { multiQuery1, multiQuery2, multiQuery3, multiQuery4 };
            MultipleResultSetInternal(db, queries);
            return Tuple.Create(multiQuery1.Results, multiQuery2.Results, multiQuery3.Results, multiQuery4.Results);
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
        [Obsolete("Use the MultipleResultSet overload that takes in MultiResult")]
        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>> MultipleResultSet<T1, T2, T3, T4, T5>(this DbContext db, IQueryable<T1> query1, IQueryable<T2> query2, IQueryable<T3> query3, IQueryable<T4> query4, IQueryable<T5> query5)
        {
            Argument.NotNull(() => query1, () => query2, () => query3, () => query4, () => query5);

            var multiQuery1 = query1.AsMultipleResultQuery();
            var multiQuery2 = query2.AsMultipleResultQuery();
            var multiQuery3 = query3.AsMultipleResultQuery();
            var multiQuery4 = query4.AsMultipleResultQuery();
            var multiQuery5 = query5.AsMultipleResultQuery();

            List<MultipleResultQuery> queries = new List<MultipleResultQuery> { multiQuery1, multiQuery2, multiQuery3, multiQuery4, multiQuery5 };
            MultipleResultSetInternal(db, queries);
            return Tuple.Create(multiQuery1.Results, multiQuery2.Results, multiQuery3.Results, multiQuery4.Results, multiQuery5.Results);
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
        [Obsolete("Use the MultipleResultSet overload that takes in MultiResult")]
        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>> MultipleResultSet<T1, T2, T3, T4, T5, T6>(this DbContext db, IQueryable<T1> query1, IQueryable<T2> query2, IQueryable<T3> query3, IQueryable<T4> query4, IQueryable<T5> query5, IQueryable<T6> query6)
        {
            Argument.NotNull(() => query1, () => query2, () => query3, () => query4, () => query5, () => query6);

            var multiQuery1 = query1.AsMultipleResultQuery();
            var multiQuery2 = query2.AsMultipleResultQuery();
            var multiQuery3 = query3.AsMultipleResultQuery();
            var multiQuery4 = query4.AsMultipleResultQuery();
            var multiQuery5 = query5.AsMultipleResultQuery();
            var multiQuery6 = query6.AsMultipleResultQuery();

            List<MultipleResultQuery> queries = new List<MultipleResultQuery> { multiQuery1, multiQuery2, multiQuery3, multiQuery4, multiQuery5, multiQuery6 };
            MultipleResultSetInternal(db, queries);
            return Tuple.Create(multiQuery1.Results, multiQuery2.Results, multiQuery3.Results, multiQuery4.Results, multiQuery5.Results, multiQuery6.Results);
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
        [Obsolete("Use the MultipleResultSet overload that takes in MultiResult")]
        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>> MultipleResultSet<T1, T2, T3, T4, T5, T6, T7>(this DbContext db, IQueryable<T1> query1, IQueryable<T2> query2, IQueryable<T3> query3, IQueryable<T4> query4, IQueryable<T5> query5, IQueryable<T6> query6, IQueryable<T7> query7)
        {
            Argument.NotNull(() => query1, () => query2, () => query3, () => query4, () => query5, () => query6, () => query7);

            var multiQuery1 = query1.AsMultipleResultQuery();
            var multiQuery2 = query2.AsMultipleResultQuery();
            var multiQuery3 = query3.AsMultipleResultQuery();
            var multiQuery4 = query4.AsMultipleResultQuery();
            var multiQuery5 = query5.AsMultipleResultQuery();
            var multiQuery6 = query6.AsMultipleResultQuery();
            var multiQuery7 = query7.AsMultipleResultQuery();

            List<MultipleResultQuery> queries = new List<MultipleResultQuery> { multiQuery1, multiQuery2, multiQuery3, multiQuery4, multiQuery5, multiQuery6, multiQuery7 };
            MultipleResultSetInternal(db, queries);
            return Tuple.Create(multiQuery1.Results, multiQuery2.Results, multiQuery3.Results, multiQuery4.Results, multiQuery5.Results, multiQuery6.Results, multiQuery7.Results);
        }

        /// <summary>
        /// Builds a single SqlCommand from the given IQueryable queries.
        /// </summary>
        /// <param name="queries">The queries to combine into a single SQL command.</param>
        /// <returns>A single, combined SQL command that returns multiple results.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "Query is generated from compiled code and is still parameterized"),
        System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Up to the called of this method to call dispose.")]
        public static SqlCommand BuildSqlCommand(IEnumerable<MultipleResultQuery> queries)
        {
            Argument.NotNull(() => queries);

            StringBuilder bulkSQLOperation = new StringBuilder();
            int parameterIndex = 0;
            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            // For each query that was passed in, visit each node in its expression tree.
            // Each time we find a parameter, keep track of that parameter so we can build
            // a collection of sql parameters to pass in with the queries.
            foreach (MultipleResultQuery query in queries)
            {
                string queryString = query.RetrieveSqlQuery(parameterIndex);
                parameterIndex = query.BuildParameters(parameterIndex, sqlParameters);
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
        /// <param name="db">The context being extended.</param>
        /// <param name="queries">A collection of queries that is to be executed.</param>
        /// <returns>The mapped result set entities.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "SQL query is parameterized")]
        private static void MultipleResultSetInternal(
            this DbContext db,
            IEnumerable<MultipleResultQuery> queries)
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
                        foreach (MultipleResultQuery query in queries)
                        {
                            query.MapResults(objectContext, reader);
                        }
                    }
                }
            }
            finally
            {
                storeConnection.Close();
            }
        }
    }
}
