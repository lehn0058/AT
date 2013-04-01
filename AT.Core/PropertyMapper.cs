using System.Reflection;

namespace AT.Core
{
    /// <summary>
    /// Handles mapping properties from one object to another.
    /// </summary>
    public static class PropertyMapper
    {
        /// <summary>
        /// Handles mapping properties from one object to another of the same type.
        /// </summary>
        /// <typeparam name="T">The type of entities being mapped.</typeparam>
        /// <param name="entityFrom">The entity to take the properties from when mapping.</param>
        /// <param name="entityTo">The entity that will have the mapped properties assigned.</param>
        public static void Map<T>(T entityFrom, T entityTo)
        {
            Argument.NotNull(() => entityFrom, () => entityTo);
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(entityFrom, null);
                if (value != null)
                {
                    property.SetValue(entityTo, value, null);
                }
            }
        }

        /// <summary>
        /// Handles mapping properties from one object to another of the same type only if the property is currently null.
        /// </summary>
        /// <typeparam name="T">The type of entities being mapped.</typeparam>
        /// <param name="entityFrom">The entity to take the properties from when mapping.</param>
        /// <param name="entityTo">The entity that will have the mapped properties assigned.</param>
        public static void MapNull<T>(T entityFrom, T entityTo)
        {
            Argument.NotNull(() => entityFrom, () => entityTo);
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            
            foreach (PropertyInfo property in properties)
            {
                object toValue = property.GetValue(entityTo, null);
                if (toValue != null)
                {
                    continue;
                }

                object fromValue = property.GetValue(entityFrom, null);
                if (fromValue != null)
                {
                    property.SetValue(entityTo, fromValue, null);
                }
            }
        }
    }
}
