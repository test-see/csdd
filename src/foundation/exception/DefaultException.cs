using System;
using System.Net;
using System.Runtime.Serialization;

namespace foundation.exception
{
    [Serializable]
    public class DefaultException : Exception
    {
        public int StatusCode { get; } = (int)HttpStatusCode.BadRequest;
        public DefaultException() { }
        public DefaultException(string message) : base(message) { }
        public DefaultException(string message, HttpStatusCode httpStatusCode) : base(message) { StatusCode = (int)httpStatusCode; }
        public DefaultException(string message, Exception inner) : base(message, inner) { }
        protected DefaultException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
