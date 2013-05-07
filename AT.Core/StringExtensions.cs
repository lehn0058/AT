using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT.Core
{
    /// <summary>
    /// Contains extension methods for the String class.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Checks to see if a string is contained within another string.
        /// </summary>
        /// <param name="original">The original string to compare.</param>
        /// <param name="value">The value to check if it is contained in the original string.</param>
        /// <param name="comparisonType">The type of comparison to be done.</param>
        /// <returns>True if the value is contained inside of the original. False otherwise.</returns>
        public static bool Contains(this string original, string value, StringComparison comparisonType)
		{
            Argument.NotNull(() => original, () => value);
			return original.IndexOf(value, comparisonType) >= 0;
		}
    }
}
