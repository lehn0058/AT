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
using System.Reflection;
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
                            query.MapResults(db, reader);
                        }
                    }
                }
            }
            finally
            {
                storeConnection.Close();
            }
        }

        /// <summary>
        /// Maps the results at the current location in the reader to a collection of type T. These results are mapped (appended)
        /// to the DbContext if possible.
        /// </summary>
        /// <typeparam name="T">The expected return type.</typeparam>
        /// <param name="customDbContext">The context doing the mapping.</param>
        /// <param name="reader">A populated reader with query results.</param>
        /// <returns>A collection of mapped entities.</returns>
        public static IEnumerable<T> Read<T>(this DbContext customDbContext, DbDataReader reader)
        {
            Argument.NotNull(() => customDbContext, () => reader);

            ObjectContext objectContext = ((IObjectContextAdapter)customDbContext).ObjectContext;
            Type currentType = customDbContext.GetType();
            Type resultType = typeof(T);
            PropertyInfo[] propertyInfos = currentType.GetProperties();
            Boolean found = false;
            List<T> results = new List<T>();

            // Look for a property that has the same return type as the one being translated.
            // There should be only one, as there can't be multiple entities in the edmx that have the same type.
            foreach (PropertyInfo info in propertyInfos)
            {
                Type type = info.PropertyType;
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(DbSet<>))
                {
                    Type propertyType = type.GetGenericArguments()[0]; // use this...

                    // Check if the current property is the same type as the expected result type.
                    if (propertyType == resultType)
                    {
                        results = objectContext.Translate<T>(reader, info.Name, MergeOption.AppendOnly).ToList();
                        reader.NextResult();
                        found = true;
                        break;
                    }
                }
            }

            // If we could not find any entities on the objectContext, still map them. These results will obviously not be available in the objectContext.
            if (!found)
            {
                results = objectContext.Translate<T>(reader).ToList();
                reader.NextResult();
            }

            return results;
        }
    }
}
