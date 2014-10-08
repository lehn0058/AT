using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Reflection;
using AT.Core;

namespace AT.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class MultipleResultMapper
    {
        readonly Dictionary<String, Int32> _mapColumnNameToColumnPosition = new Dictionary<string, int>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<string, int>> GetPropertyPositions(ObjectQuery query)
        {
            // get private ObjectQueryState ObjectQuery._state; 
            // of actual type internal class
            Object queryState = GetProperty(query, "QueryState");

            // get protected ObjectQueryExecutionPlan ObjectQueryState._cachedPlan; 
            // of actual type internal sealed class 
            Object plan = GetField(queryState, "_cachedPlan");

            // get internal readonly DbCommandDefinition ObjectQueryExecutionPlan.CommandDefinition; 
            // of actual type internal sealed class
            Object commandDefinition = GetField(plan, "CommandDefinition");

            // get private readonly IColumnMapGenerator EntityCommandDefinition._columnMapGenerator; 
            // of actual type private sealed class 
            Object columnMapGenerator = GetField(commandDefinition, "_columnMapGenerators");

            // get private readonly ColumnMap ConstantColumnMapGenerator._columnMap; 
            // of actual type internal class
            Object columnMap = GetField(((object[])columnMapGenerator)[0], "_columnMap");

            // get internal ColumnMap CollectionColumnMap.Element; 
            // of actual type internal class
            Object columnMapElement = GetProperty(columnMap, "Element");

            // get internal ColumnMap[] StructuredColumnMap.Properties; 
            // array of internal abstract class
            Array columnMapProperties = GetProperty(columnMapElement, "Properties") as Array;

            Int32 n = columnMapProperties.Length;

            KeyValuePair<string, int>[] propertyPositions = new KeyValuePair<string, int>[n];

            for (Int32 i = 0; i < n; ++i)
            {
                // get value at index i in array of actual type internal class 
                Object column = columnMapProperties.GetValue(i);
                string colName = (string)GetProperty(column, "Name");

                // get internal int ScalarColumnMap.ColumnPos; 
                Object columnPositionOfAProperty = GetProperty(column, "ColumnPos");

                propertyPositions[i] = new KeyValuePair<string, int>(colName, (int)columnPositionOfAProperty);
            }

            return propertyPositions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="readerIn"></param>
        /// <returns></returns>
        public DbDataReader WrapDataReader(ObjectQuery query, DbDataReader readerIn)
        {
            IEnumerable<KeyValuePair<string, int>> propertyPositions = GetPropertyPositions(query);
            propertyPositions.ForEach(kvp => _mapColumnNameToColumnPosition.Add(kvp.Key, kvp.Value));
            return new MappedDataReader(this, readerIn);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public int GetOrdinal(string columnName)
        {
            return _mapColumnNameToColumnPosition[columnName];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ordinal"></param>
        /// <returns></returns>
        public string GetName(int ordinal)
        {
            foreach (string strName in _mapColumnNameToColumnPosition.Keys)
            {
                if (_mapColumnNameToColumnPosition[strName] == ordinal) return strName;
            }
            return null;
        }

        private static object GetProperty(object obj, string propName)
        {
            PropertyInfo prop = obj.GetType().GetProperty(propName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (prop == null) throw EFChangedException();
            return prop.GetValue(obj, new object[0]);

        }

        private static object GetField(object obj, string fieldName, bool bThrowException = true)
        {
            FieldInfo field = obj.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null && bThrowException) throw EFChangedException();
            if (field == null) return null;
            return field.GetValue(obj);

        }

        private static InvalidOperationException EFChangedException()
        {
            return new InvalidOperationException("Entity Framework internals has changed, please review and fix reflection code");
        }
    }
}