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
            //      System.Data.Objects.ELinq.ELinqQueryState 

            Object queryState = GetProperty(query, "QueryState");
            AssertNonNullAndOfType(queryState, "System.Data.Objects.ELinq.ELinqQueryState");

            // get protected ObjectQueryExecutionPlan ObjectQueryState._cachedPlan; 
            // of actual type internal sealed class 
            //      System.Data.Objects.Internal.ObjectQueryExecutionPlan 
            Object plan = GetField(queryState, "_cachedPlan");
            AssertNonNullAndOfType(plan, "System.Data.Objects.Internal.ObjectQueryExecutionPlan");

            // get internal readonly DbCommandDefinition ObjectQueryExecutionPlan.CommandDefinition; 
            // of actual type internal sealed class 
            //      System.Data.EntityClient.EntityCommandDefinition 
            Object commandDefinition = GetField(plan, "CommandDefinition");
            AssertNonNullAndOfType(commandDefinition, "System.Data.EntityClient.EntityCommandDefinition");

            // get private readonly IColumnMapGenerator EntityCommandDefinition._columnMapGenerator; 
            // of actual type private sealed class 
            //      System.Data.EntityClient.EntityCommandDefinition.ConstantColumnMapGenerator 
            Object columnMapGenerator = GetField(commandDefinition, "_columnMapGenerator", false);
            Object columnMap;
            if (columnMapGenerator != null) // EF 4.x
            {
                AssertNonNullAndOfType(columnMapGenerator, "System.Data.EntityClient.EntityCommandDefinition+ConstantColumnMapGenerator");

                // get private readonly ColumnMap ConstantColumnMapGenerator._columnMap; 
                // of actual type internal class 
                //      System.Data.Query.InternalTrees.SimpleCollectionColumnMap 
                columnMap = GetField(columnMapGenerator, "_columnMap");
                AssertNonNullAndOfType(columnMap, "System.Data.Query.InternalTrees.SimpleCollectionColumnMap");

            }
            else // EF 5.x
            {
                // get private readonly IColumnMapGenerator EntityCommandDefinition._columnMapGenerator; 
                // of actual type private sealed class 
                //      System.Data.EntityClient.EntityCommandDefinition.ConstantColumnMapGenerator 
                columnMapGenerator = GetField(commandDefinition, "_columnMapGenerators");
                AssertNonNullAndOfType(columnMapGenerator, "System.Data.EntityClient.EntityCommandDefinition+IColumnMapGenerator[]");

                // get private readonly ColumnMap ConstantColumnMapGenerator._columnMap; 
                // of actual type internal class 
                //      System.Data.Query.InternalTrees.SimpleCollectionColumnMap 
                columnMap = GetField(((object[])columnMapGenerator)[0], "_columnMap");
                AssertNonNullAndOfType(columnMap, "System.Data.Query.InternalTrees.SimpleCollectionColumnMap");
            }

            // get internal ColumnMap CollectionColumnMap.Element; 
            // of actual type internal class 
            //      System.Data.Query.InternalTrees.RecordColumnMap 
            Object columnMapElement = GetProperty(columnMap, "Element");
            //AssertNonNullAndOfType(columnMapElement, "System.Data.Query.InternalTrees.RecordColumnMap");

            // get internal ColumnMap[] StructuredColumnMap.Properties; 
            // array of internal abstract class 
            //      System.Data.Query.InternalTrees.ColumnMap 
            Array columnMapProperties = GetProperty(columnMapElement, "Properties") as Array;
            //AssertNonNullAndOfType(columnMapProperties, "System.Data.Query.InternalTrees.ColumnMap[]");

            Int32 n = columnMapProperties.Length;

            KeyValuePair<string, int>[] propertyPositions = new KeyValuePair<string, int>[n];

            for (Int32 i = 0; i < n; ++i)
            {
                // get value at index i in array 
                // of actual type internal class 
                //      System.Data.Query.InternalTrees.ScalarColumnMap 
                Object column = columnMapProperties.GetValue(i);
                AssertNonNullAndOfType(column, "System.Data.Query.InternalTrees.ScalarColumnMap");

                string colName = (string)GetProperty(column, "Name");


                // get internal int ScalarColumnMap.ColumnPos; 
                Object columnPositionOfAProperty = GetProperty(column, "ColumnPos");
                AssertNonNullAndOfType(columnPositionOfAProperty, "System.Int32");

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

        private static void AssertNonNullAndOfType(object obj, string fullName)
        {
            if (obj == null) throw EFChangedException();
            string typeFullName = obj.GetType().FullName;
            if (typeFullName != fullName) throw EFChangedException();

        }

        private static InvalidOperationException EFChangedException()
        {
            return new InvalidOperationException("Entity Framework internals has changed, please review and fix reflection code");

        }
    }
}