using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT.Core
{
    /// <summary>
    /// Allows you to generically send any object type as a value following the EventArgs pattern.
    /// </summary>
    /// <typeparam name="T">The type to be wrapped.</typeparam>
    public class EventArgs<T> : EventArgs
    {
        private T _value;

        /// <summary>
        /// Creates a new EventArgs that wraps a value of type T.
        /// </summary>
        /// <param name="value">The value to pass along in the event args.</param>
        public EventArgs(T value)
        {
            _value = value;
        }

        /// <summary>
        /// The value passed along in the event args.
        /// </summary>
        public T Value
        {
            get { return _value; }
        }
    }
}
