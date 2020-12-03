using System;
using System.Runtime.Serialization;

namespace foundation.exception
{
    [Serializable]
    public class DefaultException : Exception
    {
        public DefaultException() { }
        public DefaultException(string message) : base(message) { }
        public DefaultException(string message, Exception inner) : base(message, inner) { }
        protected DefaultException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
