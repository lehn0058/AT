using AT.Core;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Objects;
using System.Linq;

namespace AT.Data
{
    /// <summary>
    /// Extension methods for the System.Data.Objects.ObjectContext class.
    /// </summary>
    public static class ObjectContextExtensions
    {
        /// <summary>
        /// Compact translation of reader results into an object.
        /// </summary>
        /// <typeparam name="T">The type of entities we are expecting the reader to return.</typeparam>
        /// <param name="objectContext">The object context we are extending.</param>
        /// <param name="reader">A populated data reader popualted with database results.</param>
        /// <returns>A collection of mapped entities that were read from the data reader.</returns>
        public static IEnumerable<T> Read<T>(this ObjectContext objectContext, DbDataReader reader)
        {
            Argument.NotNull(() => objectContext, () => reader);

            List<T> result = objectContext.Translate<T>(reader).ToList();
            reader.NextResult();
            return result;
        }

        /// <summary>
        /// Compact translation of reader results into an object.
        /// </summary>
        /// <typeparam name="T1">The type of entities we are expecting the reader to return.</typeparam>
        /// <typeparam name="T2">The type of entities we are expecting the reader to return.</typeparam>
        /// <param name="objectContext">The object context we are extending.</param>
        /// <param name="reader">A populated data reader popualted with database results.</param>
        /// <returns>A collection of mapped entities that were read from the data reader.</returns>
        public static Tuple<IEnumerable<T1>, IEnumerable<T2>> Read<T1, T2>(this ObjectContext objectContext, DbDataReader reader)
        {
            Argument.NotNull(() => objectContext, () => reader);

            return Tuple.Create(
                Read<T1>(objectContext, reader),
                Read<T2>(objectContext, reader));
        }

        /// <summary>
        /// Compact translation of reader results into an object.
        /// </summary>
        /// <typeparam name="T1">The type of entities we are expecting the reader to return.</typeparam>
        /// <typeparam name="T2">The type of entities we are expecting the reader to return.</typeparam>
        /// <typeparam name="T3">The type of entities we are expecting the reader to return.</typeparam>
        /// <param name="objectContext">The object context we are extending.</param>
        /// <param name="reader">A populated data reader popualted with database results.</param>
        /// <returns>A collection of mapped entities that were read from the data reader.</returns>
        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> Read<T1, T2, T3>(this ObjectContext objectContext, DbDataReader reader)
        {
            Argument.NotNull(() => objectContext, () => reader);

            return Tuple.Create(
                Read<T1>(objectContext, reader),
                Read<T2>(objectContext, reader),
                Read<T3>(objectContext, reader));
        }

        /// <summary>
        /// Compact translation of reader results into an object.
        /// </summary>
        /// <typeparam name="T1">The type of entities we are expecting the reader to return.</typeparam>
        /// <typeparam name="T2">The type of entities we are expecting the reader to return.</typeparam>
        /// <typeparam name="T3">The type of entities we are expecting the reader to return.</typeparam>
        /// <typeparam name="T4">The type of entities we are expecting the reader to return.</typeparam>
        /// <param name="objectContext">The object context we are extending.</param>
        /// <param name="reader">A populated data reader popualted with database results.</param>
        /// <returns>A collection of mapped entities that were read from the data reader.</returns>
        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>> Read<T1, T2, T3, T4>(this ObjectContext objectContext, DbDataReader reader)
        {
            Argument.NotNull(() => objectContext, () => reader);

            return Tuple.Create(
                Read<T1>(objectContext, reader),
                Read<T2>(objectContext, reader),
                Read<T3>(objectContext, reader),
                Read<T4>(objectContext, reader));
        }

        /// <summary>
        /// Compact translation of reader results into an object.
        /// </summary>
        /// <typeparam name="T1">The type of entities we are expecting the reader to return.</typeparam>
        /// <typeparam name="T2">The type of entities we are expecting the reader to return.</typeparam>
        /// <typeparam name="T3">The type of entities we are expecting the reader to return.</typeparam>
        /// <typeparam name="T4">The type of entities we are expecting the reader to return.</typeparam>
        /// <typeparam name="T5">The type of entities we are expecting the reader to return.</typeparam>
        /// <param name="objectContext">The object context we are extending.</param>
        /// <param name="reader">A populated data reader popualted with database results.</param>
        /// <returns>A collection of mapped entities that were read from the data reader.</returns>
        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>> Read<T1, T2, T3, T4, T5>(this ObjectContext objectContext, DbDataReader reader)
        {
            Argument.NotNull(() => objectContext, () => reader);

            return Tuple.Create(
                Read<T1>(objectContext, reader),
                Read<T2>(objectContext, reader),
                Read<T3>(objectContext, reader),
                Read<T4>(objectContext, reader),
                Read<T5>(objectContext, reader));
        }

        /// <summary>
        /// Compact translation of reader results into an object.
        /// </summary>
        /// <typeparam name="T1">The type of entities we are expecting the reader to return.</typeparam>
        /// <typeparam name="T2">The type of entities we are expecting the reader to return.</typeparam>
        /// <typeparam name="T3">The type of entities we are expecting the reader to return.</typeparam>
        /// <typeparam name="T4">The type of entities we are expecting the reader to return.</typeparam>
        /// <typeparam name="T5">The type of entities we are expecting the reader to return.</typeparam>
        /// <typeparam name="T6">The type of entities we are expecting the reader to return.</typeparam>
        /// <param name="objectContext">The object context we are extending.</param>
        /// <param name="reader">A populated data reader popualted with database results.</param>
        /// <returns>A collection of mapped entities that were read from the data reader.</returns>
        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>> Read<T1, T2, T3, T4, T5, T6>(this ObjectContext objectContext, DbDataReader reader)
        {
            Argument.NotNull(() => objectContext, () => reader);

            return Tuple.Create(
                Read<T1>(objectContext, reader),
                Read<T2>(objectContext, reader),
                Read<T3>(objectContext, reader),
                Read<T4>(objectContext, reader),
                Read<T5>(objectContext, reader),
                Read<T6>(objectContext, reader));
        }

        /// <summary>
        /// Compact translation of reader results into an object.
        /// </summary>
        /// <typeparam name="T1">The type of entities we are expecting the reader to return.</typeparam>
        /// <typeparam name="T2">The type of entities we are expecting the reader to return.</typeparam>
        /// <typeparam name="T3">The type of entities we are expecting the reader to return.</typeparam>
        /// <typeparam name="T4">The type of entities we are expecting the reader to return.</typeparam>
        /// <typeparam name="T5">The type of entities we are expecting the reader to return.</typeparam>
        /// <typeparam name="T6">The type of entities we are expecting the reader to return.</typeparam>
        /// <typeparam name="T7">The type of entities we are expecting the reader to return.</typeparam>
        /// <param name="objectContext">The object context we are extending.</param>
        /// <param name="reader">A populated data reader popualted with database results.</param>
        /// <returns>A collection of mapped entities that were read from the data reader.</returns>
        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>> Read<T1, T2, T3, T4, T5, T6, T7>(this ObjectContext objectContext, DbDataReader reader)
        {
            Argument.NotNull(() => objectContext, () => reader);

            return Tuple.Create(
                Read<T1>(objectContext, reader),
                Read<T2>(objectContext, reader),
                Read<T3>(objectContext, reader),
                Read<T4>(objectContext, reader),
                Read<T5>(objectContext, reader),
                Read<T6>(objectContext, reader),
                Read<T7>(objectContext, reader));
        }
    }
}
