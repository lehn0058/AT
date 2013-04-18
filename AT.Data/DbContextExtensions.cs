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
