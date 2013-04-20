using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AT.Core
{
    /// <summary>
    /// Provides compact parameter and function result validation.
    /// </summary>
    public static class Argument
    {
        #region Null

        /// <summary>
        /// Validates the given expression returns null. Raises an exception if it does not.
        /// </summary>
        /// <typeparam name="T">The result type returned by the function.</typeparam>
        /// <param name="expression">The function to evaluate.</param>
        /// <returns>The value of the evaluated function.</returns>
        [DebuggerHidden]
        public static T Null<T>(Func<T> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression", "Parameter was null. Did you pass it in as a function?");
            }

            T parameter = expression();

            if (parameter != null)
            {
                // Throw an exception with the name of the original parameter that was passed in
                throw new ArgumentException("parameter was not null", RetrievePreviousParameterName(expression));
            }

            return parameter;
        }

        /// <summary>
        /// Validates the given expression returns null. Raises an exception if it does not.
        /// Allows a user to pass in N number of parameters as long as they are the same type.
        /// </summary>
        /// <param name="expressions">The functions to evaluate.</param>
        [DebuggerHidden]
        public static void Null(params Func<object>[] expressions)
        {
            NotNull(() => expressions);
            expressions.ForEach(expression => Null<object>(expression));
        }

        #endregion

        #region NotNull

        /// <summary>
        /// Validates the given expression does not return null. Raises an exception if it does.
        /// </summary>
        /// <typeparam name="T">The result type returned by the function.</typeparam>
        /// <param name="expression">The function to evaluate.</param>
        /// <returns>The value of the evaluated function.</returns>
        [DebuggerHidden]
        public static T NotNull<T>(Func<T> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression", "Parameter was null. Did you pass it in as a function?");
            }

            T parameter = expression();

            if (parameter == null)
            {
                // Throw an exception with the name of the original parameter that was passed in
                throw new ArgumentNullException(RetrievePreviousParameterName(expression), "parameter was null");
            }

            return parameter;
        }

        /// <summary>
        /// Validates the given expression does not return null. Raises an exception if it does.
        /// Allows a user to pass in N number of parameters as long as they are the same type.
        /// </summary>
        /// <param name="expressions">The functions to evaluate.</param>
        [DebuggerHidden]
        public static void NotNull(params Func<object>[] expressions)
        {
            NotNull(() => expressions);
            expressions.ForEach(expression => NotNull<object>(expression));
        }

        #endregion

        #region NotNullOrEmpty

        #region NotNullOrEmpty<String>

        /// <summary>
        /// Validates the given expression does not return null or an empty string. Raises an exception if it does.
        /// </summary>
        /// <param name="expression">The function to evaluate.</param>
        /// <returns>The value of the evaluated function.</returns>
        [DebuggerHidden]
        public static String NotNullOrEmpty(Func<String> expression)
        {
            String parameter = NotNull(expression);

            if (String.IsNullOrEmpty(parameter))
            {
                // Throw an exception with the name of the original parameter that was passed in
                throw new ArgumentException("parameter was empty", RetrievePreviousParameterName(expression));
            }

            return parameter;
        }

        /// <summary>
        /// Validates the given expression does not return null or an empty string. Raises an exception if it does.
        /// </summary>
        /// <param name="expressions">Function 1 to evaluate.</param>
        [DebuggerHidden]
        public static void NotNullOrEmpty(params Func<String>[] expressions)
        {
            NotNull(() => expressions);
            expressions.ForEach(expression => NotNullOrEmpty(expression));
        }

        #endregion

        #region NotNullOrEmpty<Guid>

        /// <summary>
        /// Validates the given expression does not return null or an empty Guid. Raises an exception if it does.
        /// </summary>
        /// <param name="expression">The function to evaluate.</param>
        /// <returns>The value of the evaluated function.</returns>
        [DebuggerHidden]
        public static Guid NotNullOrEmpty(Func<Guid> expression)
        {
            Guid parameter = NotNull(expression);

            if (parameter.Equals(Guid.Empty))
            {
                throw new ArgumentException("parameter was empty", RetrievePreviousParameterName(expression));
            }

            return parameter;
        }

        /// <summary>
        /// Validates the given expression does not return null or an empty Guid. Raises an exception if it does.
        /// </summary>
        /// <param name="expressions">Functions to evaluate.</param>
        [DebuggerHidden]
        public static void NotNullOrEmpty(params Func<Guid>[] expressions)
        {
            NotNull(() => expressions);
            expressions.ForEach(expression => NotNullOrEmpty(expression));
        }

        #endregion

        #region NullOrEmpty<IEnumerable<T>>

        /// <summary>
        /// Validates the given expression does not return null or an empty collection. Raises an exception if it does.
        /// </summary>
        /// <param name="expression">The function to evaluate.</param>
        /// <returns>The value of the evaluated function.</returns>
        [DebuggerHidden]
        public static IEnumerable NotNullOrEmpty(Func<IEnumerable> expression)
        {
            IEnumerable parameter = NotNull(expression);

            IEnumerator enumerator = parameter.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("parameter was empty", RetrievePreviousParameterName(expression));
            }

            return parameter;
        }

        /// <summary>
        /// Validates the given expression does not return null or an empty collection. Raises an exception if it does.
        /// </summary>
        /// <param name="expressions">The function to evaluate.</param>
        [DebuggerHidden]
        public static void NotNullOrEmpty(params Func<IEnumerable>[] expressions)
        {
            Argument.NotNull(() => expressions);
            expressions.ForEach(expression => Argument.NotNullOrEmpty(expression));
        }

        #endregion

        #endregion

        #region NotNullOrWhiteSpace

        /// <summary>
        /// Validates the given expression does not return null or an all whitespace string. Raises an exception if it does.
        /// </summary>
        /// <param name="expression">The function to evaluate.</param>
        /// <returns>The value of the evaluated function.</returns>
        [DebuggerHidden]
        public static String NotNullOrWhiteSpace(Func<String> expression)
        {
            String parameter = NotNull(expression);

            if (String.IsNullOrWhiteSpace(parameter))
            {
                throw new ArgumentException("parameter was only white space", RetrievePreviousParameterName(expression));
            }

            return parameter;
        }

        /// <summary>
        /// Validates the given expression does not return null or an all whitespace string. Raises an exception if it does.
        /// </summary>
        /// <param name="expressions">Functions to evaluate.</param>
        [DebuggerHidden]
        public static void NotNullOrWhiteSpace(params Func<String>[] expressions)
        {
            NotNull(() => expressions);
            expressions.ForEach(expression => NotNullOrWhiteSpace(expression));
        }

        #endregion

        #region NotNullOrEmptyOrWhiteSpace

        /// <summary>
        /// Validates the given expression does not return null or an empty string or an all whitespace string. Raises an exception if it does.
        /// </summary>
        /// <param name="expression">The function to evaluate.</param>
        /// <returns>The value of the evaluated function.</returns>
        [DebuggerHidden]
        public static String NotNullOrEmptyOrWhiteSpace(Func<String> expression)
        {
            String parameter = NotNullOrEmpty(expression);

            if (String.IsNullOrWhiteSpace(parameter))
            {
                throw new ArgumentException("parameter contained only white space", RetrievePreviousParameterName(expression));
            }

            return parameter;
        }

        /// <summary>
        /// Validates the given expression does not return null or an empty string or an all whitespace string. Raises an exception if it does.
        /// </summary>
        /// <param name="expressions">Functions to evaluate.</param>
        [DebuggerHidden]
        public static void NotNullOrEmptyOrWhiteSpace(params Func<String>[] expressions)
        {
            NotNull(() => expressions);
            expressions.ForEach(expression => NotNullOrEmptyOrWhiteSpace(expression));
        }

        #endregion

        #region NotNullIsBetween

        /// <summary>
        /// Validates that the given expression is not null and falls between the given lower and upper bounds.
        /// This comparison is inclusive.
        /// </summary>
        /// <typeparam name="T">The type to be compared.</typeparam>
        /// <param name="lowerBound">The lower bound used in the range check</param>
        /// <param name="upperBound">The upper bound used in the range check.</param>
        /// <param name="expression">The function to evaluate</param>
        /// <returns>The value of the evaulated function.</returns>
        [DebuggerHidden]
        public static T NotNullIsBetween<T>(T lowerBound, T upperBound, Func<T> expression)
        {
            Argument.NotNull(() => lowerBound, () => upperBound);

            T parameter = Argument.NotNull(expression);

            // If our value if not larger than the lower bound, then we are out of range
            if (!(Comparer<T>.Default.Compare(parameter, lowerBound) >= 0))
            {
                throw new ArgumentOutOfRangeException(RetrievePreviousParameterName(expression), "the parameter was less that the desired range");
            }

            // If our value if not smaller than the upper bound, then we are out of range
            if (!(Comparer<T>.Default.Compare(parameter, upperBound) <= 0))
            {
                throw new ArgumentOutOfRangeException(RetrievePreviousParameterName(expression), "the parameter was larger that the desired range");
            }

            return parameter;
        }

        /// <summary>
        /// Validates that the given expressions are not null and falls between the given lower and upper bounds.
        /// This comparison is inclusive.
        /// </summary>
        /// <typeparam name="T">The type to be compared.</typeparam>
        /// <param name="lowerBound">The lower bound used in the range check</param>
        /// <param name="upperBound">The upper bound used in the range check.</param>
        /// <param name="expressions">The functions to evaluate</param>
        [DebuggerHidden]
        public static void NotNullIsBetween<T>(T lowerBound, T upperBound, params Func<T>[] expressions)
        {
            NotNull(() => expressions);
            expressions.ForEach(expression => NotNullIsBetween(lowerBound, upperBound, expression));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Retrieves the name of the parameter that was wrapped inside the given function.
        /// </summary>
        /// <typeparam name="T">The parameter type that is wrapped in this function.</typeparam>
        /// <param name="expression">The function wrapping a parameter.</param>
        /// <returns>The string name of the wrapped parameter.</returns>
        private static String RetrievePreviousParameterName<T>(Func<T> expression)
        {
            // If the expression target is null, then this was probably a constant.
            // This really shouldn't need to be tested for null, and we can't get it's original name.
            if (expression.Target == null)
            {
                return "unknown parameter name - was this a constant value?";
            }

            byte[] ils = expression.Method.GetMethodBody().GetILAsByteArray();
            int fieldHandle = BitConverter.ToInt32(ils, 2);
            return expression.Target.GetType().Module.ResolveField(fieldHandle).Name;
        }

        #endregion
    }
}
