using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT.Core
{
    /// <summary>
    /// Extension methods for the IEnumerable interface.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Enumerates each element in the source and performs the given action on them.
        /// </summary>
        /// <typeparam name="T">The type of element in the source collection.</typeparam>
        /// <param name="source">The collection of elements to have an action performed on.</param>
        /// <param name="action">The action to be performed on each element.</param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            Argument.NotNull<IEnumerable<T>>(() => source);
            Argument.NotNull<Action<T>>(() => action);

            IEnumerator<T> enumerator = source.GetEnumerator();

            while (enumerator.MoveNext())
            {
                action.Invoke(enumerator.Current);
            }
        }
    }
}
