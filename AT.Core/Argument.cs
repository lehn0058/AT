using System;
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
        /// <typeparam name="T">The result type returned by the function.</typeparam>
        /// <param name="expressions">The functions to evaluate.</param>
        [DebuggerHidden]
        public static void NotNull<T>(params Func<T>[] expressions)
        {
            NotNull(() => expressions);

            foreach (Func<T> expression in expressions)
            {
                NotNull(expression);
            }
        }

        /// <summary>
        /// Validates the given expression does not return null. Raises an exception if it does.
        /// </summary>
        /// <typeparam name="T1">The result type returned from function 1</typeparam>
        /// <typeparam name="T2">The result type returned from function 2.</typeparam>
        /// <param name="expression1">Function 1 to evaluate.</param>
        /// <param name="expression2">Function 2 to evaluate.</param>
        [DebuggerHidden]
        public static void NotNull<T1, T2>(Func<T1> expression1, Func<T2> expression2)
        {
            NotNull(expression1);
            NotNull(expression2);
        }

        /// <summary>
        /// Validates the given expression does not return null. Raises an exception if it does.
        /// </summary>
        /// <typeparam name="T1">The result type returned from function 1</typeparam>
        /// <typeparam name="T2">The result type returned from function 2.</typeparam>
        /// <typeparam name="T3">The result type returned from function 3.</typeparam>
        /// <param name="expression1">Function 1 to evaluate.</param>
        /// <param name="expression2">Function 2 to evaluate.</param>
        /// <param name="expression3">Function 3 to evaluate.</param>
        [DebuggerHidden]
        public static void NotNull<T1, T2, T3>(Func<T1> expression1, Func<T2> expression2, Func<T3> expression3)
        {
            NotNull(expression1);
            NotNull(expression2, expression3);
        }

        /// <summary>
        /// Validates the given expression does not return null. Raises an exception if it does.
        /// </summary>
        /// <typeparam name="T1">The result type returned from function 1</typeparam>
        /// <typeparam name="T2">The result type returned from function 2.</typeparam>
        /// <typeparam name="T3">The result type returned from function 3.</typeparam>
        /// <typeparam name="T4">The result type returned from function 4.</typeparam>
        /// <param name="expression1">Function 1 to evaluate.</param>
        /// <param name="expression2">Function 2 to evaluate.</param>
        /// <param name="expression3">Function 3 to evaluate.</param>
        /// <param name="expression4">Function 4 to evaluate.</param>
        [DebuggerHidden]
        public static void NotNull<T1, T2, T3, T4>(Func<T1> expression1, Func<T2> expression2, Func<T3> expression3, Func<T4> expression4)
        {
            NotNull(expression1);
            NotNull(expression2, expression3, expression4);
        }

        /// <summary>
        /// Validates the given expression does not return null. Raises an exception if it does.
        /// </summary>
        /// <typeparam name="T1">The result type returned from function 1</typeparam>
        /// <typeparam name="T2">The result type returned from function 2.</typeparam>
        /// <typeparam name="T3">The result type returned from function 3.</typeparam>
        /// <typeparam name="T4">The result type returned from function 4.</typeparam>
        /// <typeparam name="T5">The result type returned from function 5.</typeparam>
        /// <param name="expression1">Function 1 to evaluate.</param>
        /// <param name="expression2">Function 2 to evaluate.</param>
        /// <param name="expression3">Function 3 to evaluate.</param>
        /// <param name="expression4">Function 4 to evaluate.</param>
        /// <param name="expression5">Function 5 to evaluate.</param>
        [DebuggerHidden]
        public static void NotNull<T1, T2, T3, T4, T5>(Func<T1> expression1, Func<T2> expression2, Func<T3> expression3, Func<T4> expression4, Func<T5> expression5)
        {
            NotNull(expression1);
            NotNull(expression2, expression3, expression4, expression5);
        }

        /// <summary>
        /// Validates the given expression does not return null. Raises an exception if it does.
        /// </summary>
        /// <typeparam name="T1">The result type returned from function 1</typeparam>
        /// <typeparam name="T2">The result type returned from function 2.</typeparam>
        /// <typeparam name="T3">The result type returned from function 3.</typeparam>
        /// <typeparam name="T4">The result type returned from function 4.</typeparam>
        /// <typeparam name="T5">The result type returned from function 5.</typeparam>
        /// <typeparam name="T6">The result type returned from function 6.</typeparam>
        /// <param name="expression1">Function 1 to evaluate.</param>
        /// <param name="expression2">Function 2 to evaluate.</param>
        /// <param name="expression3">Function 3 to evaluate.</param>
        /// <param name="expression4">Function 4 to evaluate.</param>
        /// <param name="expression5">Function 5 to evaluate.</param>
        /// <param name="expression6">Function 6 to evaluate.</param>
        [DebuggerHidden]
        public static void NotNull<T1, T2, T3, T4, T5, T6>(Func<T1> expression1, Func<T2> expression2, Func<T3> expression3, Func<T4> expression4, Func<T5> expression5, Func<T6> expression6)
        {
            NotNull(expression1);
            NotNull(expression2, expression3, expression4, expression5, expression6);
        }

        /// <summary>
        /// Validates the given expression does not return null. Raises an exception if it does.
        /// </summary>
        /// <typeparam name="T1">The result type returned from function 1</typeparam>
        /// <typeparam name="T2">The result type returned from function 2.</typeparam>
        /// <typeparam name="T3">The result type returned from function 3.</typeparam>
        /// <typeparam name="T4">The result type returned from function 4.</typeparam>
        /// <typeparam name="T5">The result type returned from function 5.</typeparam>
        /// <typeparam name="T6">The result type returned from function 6.</typeparam>
        /// <typeparam name="T7">The result type returned from function 7.</typeparam>
        /// <param name="expression1">Function 1 to evaluate.</param>
        /// <param name="expression2">Function 2 to evaluate.</param>
        /// <param name="expression3">Function 3 to evaluate.</param>
        /// <param name="expression4">Function 4 to evaluate.</param>
        /// <param name="expression5">Function 5 to evaluate.</param>
        /// <param name="expression6">Function 6 to evaluate.</param>
        /// <param name="expression7">Function 7 to evaluate.</param>
        [DebuggerHidden]
        public static void NotNull<T1, T2, T3, T4, T5, T6, T7>(Func<T1> expression1, Func<T2> expression2, Func<T3> expression3, Func<T4> expression4, Func<T5> expression5, Func<T6> expression6, Func<T7> expression7)
        {
            NotNull(expression1);
            NotNull(expression2, expression3, expression4, expression5, expression6, expression7);
        }

        /// <summary>
        /// Validates the given expression does not return null. Raises an exception if it does.
        /// </summary>
        /// <typeparam name="T1">The result type returned from function 1</typeparam>
        /// <typeparam name="T2">The result type returned from function 2.</typeparam>
        /// <typeparam name="T3">The result type returned from function 3.</typeparam>
        /// <typeparam name="T4">The result type returned from function 4.</typeparam>
        /// <typeparam name="T5">The result type returned from function 5.</typeparam>
        /// <typeparam name="T6">The result type returned from function 6.</typeparam>
        /// <typeparam name="T7">The result type returned from function 7.</typeparam>
        /// <typeparam name="T8">The result type returned from function 8.</typeparam>
        /// <param name="expression1">Function 1 to evaluate.</param>
        /// <param name="expression2">Function 2 to evaluate.</param>
        /// <param name="expression3">Function 3 to evaluate.</param>
        /// <param name="expression4">Function 4 to evaluate.</param>
        /// <param name="expression5">Function 5 to evaluate.</param>
        /// <param name="expression6">Function 6 to evaluate.</param>
        /// <param name="expression7">Function 7 to evaluate.</param>
        /// <param name="expression8">Function 8 to evaluate.</param>
        [DebuggerHidden]
        public static void NotNull<T1, T2, T3, T4, T5, T6, T7, T8>(Func<T1> expression1, Func<T2> expression2, Func<T3> expression3, Func<T4> expression4, Func<T5> expression5, Func<T6> expression6, Func<T7> expression7, Func<T8> expression8)
        {
            NotNull(expression1);
            NotNull(expression2, expression3, expression4, expression5, expression6, expression7, expression8);
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

            foreach (Func<String> expression in expressions)
            {
                NotNullOrEmpty(expression);    
            }
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

            foreach (Func<Guid> expression in expressions)
            {
                NotNullOrEmpty(expression);
            }
        }

        #endregion

        #region NullOrEmpty<IEnumerable<T>>

        /// <summary>
        /// Validates the given expression does not return null or an empty collection. Raises an exception if it does.
        /// </summary>
        /// <param name="expression">The function to evaluate.</param>
        /// <returns>The value of the evaluated function.</returns>
        [DebuggerHidden]
        public static IEnumerable<T> NotNullOrEmpty<T>(Func<IEnumerable<T>> expression)
        {
            IEnumerable<T> parameter = NotNull(expression);

            if (!parameter.Any())
            {
                throw new ArgumentException("parameter was empty", RetrievePreviousParameterName(expression));
            }

            return parameter;
        }

        /// <summary>
        /// Validates the given expression does not return null or an empty collection. Raises an exception if it does.
        /// </summary>
        /// <typeparam name="T1">The result type returned from function 1</typeparam>
        /// <typeparam name="T2">The result type returned from function 2.</typeparam>
        /// <param name="expression1">Function 1 to evaluate.</param>
        /// <param name="expression2">Function 2 to evaluate.</param>
        [DebuggerHidden]
        public static void NotNullOrEmpty<T1, T2>(Func<IEnumerable<T1>> expression1, Func<IEnumerable<T2>> expression2)
        {
            NotNullOrEmpty(expression1);
            NotNullOrEmpty(expression2);
        }

        /// <summary>
        /// Validates the given expression does not return null or an empty collection. Raises an exception if it does.
        /// </summary>
        /// <typeparam name="T1">The result type returned from function 1</typeparam>
        /// <typeparam name="T2">The result type returned from function 2.</typeparam>
        /// <typeparam name="T3">The result type returned from function 3.</typeparam>
        /// <param name="expression1">Function 1 to evaluate.</param>
        /// <param name="expression2">Function 2 to evaluate.</param>
        /// <param name="expression3">Function 3 to evaluate.</param>
        [DebuggerHidden]
        public static void NotNullOrEmpty<T1, T2, T3>(Func<IEnumerable<T1>> expression1, Func<IEnumerable<T2>> expression2, Func<IEnumerable<T3>> expression3)
        {
            NotNullOrEmpty(expression1);
            NotNullOrEmpty(expression2, expression3);
        }

        /// <summary>
        /// Validates the given expression does not return null or an empty collection. Raises an exception if it does.
        /// </summary>
        /// <typeparam name="T1">The result type returned from function 1</typeparam>
        /// <typeparam name="T2">The result type returned from function 2.</typeparam>
        /// <typeparam name="T3">The result type returned from function 3.</typeparam>
        /// <typeparam name="T4">The result type returned from function 4.</typeparam>
        /// <param name="expression1">Function 1 to evaluate.</param>
        /// <param name="expression2">Function 2 to evaluate.</param>
        /// <param name="expression3">Function 3 to evaluate.</param>
        /// <param name="expression4">Function 4 to evaluate.</param>
        [DebuggerHidden]
        public static void NotNullOrEmpty<T1, T2, T3, T4>(Func<IEnumerable<T1>> expression1, Func<IEnumerable<T2>> expression2, Func<IEnumerable<T3>> expression3, Func<IEnumerable<T4>> expression4)
        {
            NotNullOrEmpty(expression1);
            NotNullOrEmpty(expression2, expression3, expression4);
        }

        /// <summary>
        /// Validates the given expression does not return null or an empty collection. Raises an exception if it does.
        /// </summary>
        /// <typeparam name="T1">The result type returned from function 1</typeparam>
        /// <typeparam name="T2">The result type returned from function 2.</typeparam>
        /// <typeparam name="T3">The result type returned from function 3.</typeparam>
        /// <typeparam name="T4">The result type returned from function 4.</typeparam>
        /// <typeparam name="T5">The result type returned from function 5.</typeparam>
        /// <param name="expression1">Function 1 to evaluate.</param>
        /// <param name="expression2">Function 2 to evaluate.</param>
        /// <param name="expression3">Function 3 to evaluate.</param>
        /// <param name="expression4">Function 4 to evaluate.</param>
        /// <param name="expression5">Function 5 to evaluate.</param>
        [DebuggerHidden]
        public static void NotNullOrEmpty<T1, T2, T3, T4, T5>(Func<IEnumerable<T1>> expression1, Func<IEnumerable<T2>> expression2, Func<IEnumerable<T3>> expression3, Func<IEnumerable<T4>> expression4, Func<IEnumerable<T5>> expression5)
        {
            NotNullOrEmpty(expression1);
            NotNullOrEmpty(expression2, expression3, expression4, expression5);
        }

        /// <summary>
        /// Validates the given expression does not return null or an empty collection. Raises an exception if it does.
        /// </summary>
        /// <typeparam name="T1">The result type returned from function 1</typeparam>
        /// <typeparam name="T2">The result type returned from function 2.</typeparam>
        /// <typeparam name="T3">The result type returned from function 3.</typeparam>
        /// <typeparam name="T4">The result type returned from function 4.</typeparam>
        /// <typeparam name="T5">The result type returned from function 5.</typeparam>
        /// <typeparam name="T6">The result type returned from function 6.</typeparam>
        /// <param name="expression1">Function 1 to evaluate.</param>
        /// <param name="expression2">Function 2 to evaluate.</param>
        /// <param name="expression3">Function 3 to evaluate.</param>
        /// <param name="expression4">Function 4 to evaluate.</param>
        /// <param name="expression5">Function 5 to evaluate.</param>
        /// <param name="expression6">Function 6 to evaluate.</param>
        [DebuggerHidden]
        public static void NotNullOrEmpty<T1, T2, T3, T4, T5, T6>(Func<IEnumerable<T1>> expression1, Func<IEnumerable<T2>> expression2, Func<IEnumerable<T3>> expression3, Func<IEnumerable<T4>> expression4, Func<IEnumerable<T5>> expression5, Func<IEnumerable<T6>> expression6)
        {
            NotNullOrEmpty(expression1);
            NotNullOrEmpty(expression2, expression3, expression4, expression5, expression6);
        }

        /// <summary>
        /// Validates the given expression does not return null or an empty collection. Raises an exception if it does.
        /// </summary>
        /// <typeparam name="T1">The result type returned from function 1</typeparam>
        /// <typeparam name="T2">The result type returned from function 2.</typeparam>
        /// <typeparam name="T3">The result type returned from function 3.</typeparam>
        /// <typeparam name="T4">The result type returned from function 4.</typeparam>
        /// <typeparam name="T5">The result type returned from function 5.</typeparam>
        /// <typeparam name="T6">The result type returned from function 6.</typeparam>
        /// <typeparam name="T7">The result type returned from function 7.</typeparam>
        /// <param name="expression1">Function 1 to evaluate.</param>
        /// <param name="expression2">Function 2 to evaluate.</param>
        /// <param name="expression3">Function 3 to evaluate.</param>
        /// <param name="expression4">Function 4 to evaluate.</param>
        /// <param name="expression5">Function 5 to evaluate.</param>
        /// <param name="expression6">Function 6 to evaluate.</param>
        /// <param name="expression7">Function 7 to evaluate.</param>
        [DebuggerHidden]
        public static void NotNullOrEmpty<T1, T2, T3, T4, T5, T6, T7>(Func<IEnumerable<T1>> expression1, Func<IEnumerable<T2>> expression2, Func<IEnumerable<T3>> expression3, Func<IEnumerable<T4>> expression4, Func<IEnumerable<T5>> expression5, Func<IEnumerable<T6>> expression6, Func<IEnumerable<T7>> expression7)
        {
            NotNullOrEmpty(expression1);
            NotNullOrEmpty(expression2, expression3, expression4, expression5, expression6, expression7);
        }

        /// <summary>
        /// Validates the given expression does not return null or an empty collection. Raises an exception if it does.
        /// </summary>
        /// <typeparam name="T1">The result type returned from function 1</typeparam>
        /// <typeparam name="T2">The result type returned from function 2.</typeparam>
        /// <typeparam name="T3">The result type returned from function 3.</typeparam>
        /// <typeparam name="T4">The result type returned from function 4.</typeparam>
        /// <typeparam name="T5">The result type returned from function 5.</typeparam>
        /// <typeparam name="T6">The result type returned from function 6.</typeparam>
        /// <typeparam name="T7">The result type returned from function 7.</typeparam>
        /// <typeparam name="T8">The result type returned from function 8.</typeparam>
        /// <param name="expression1">Function 1 to evaluate.</param>
        /// <param name="expression2">Function 2 to evaluate.</param>
        /// <param name="expression3">Function 3 to evaluate.</param>
        /// <param name="expression4">Function 4 to evaluate.</param>
        /// <param name="expression5">Function 5 to evaluate.</param>
        /// <param name="expression6">Function 6 to evaluate.</param>
        /// <param name="expression7">Function 7 to evaluate.</param>
        /// <param name="expression8">Function 8 to evaluate.</param>
        [DebuggerHidden]
        public static void NotNullOrEmpty<T1, T2, T3, T4, T5, T6, T7, T8>(Func<IEnumerable<T1>> expression1, Func<IEnumerable<T2>> expression2, Func<IEnumerable<T3>> expression3, Func<IEnumerable<T4>> expression4, Func<IEnumerable<T5>> expression5, Func<IEnumerable<T6>> expression6, Func<IEnumerable<T7>> expression7, Func<IEnumerable<T8>> expression8)
        {
            NotNullOrEmpty(expression1);
            NotNullOrEmpty(expression2, expression3, expression4, expression5, expression6, expression7, expression8);
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

            foreach (Func<String> expression in expressions)
            {
                NotNullOrWhiteSpace(expression);
            }
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

            foreach (Func<String> expression in expressions)
            {
                NotNullOrEmptyOrWhiteSpace(expression);
            }
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
