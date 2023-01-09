using System;
using System.Runtime.Serialization;

namespace WebApp_Authentication_API
{
    [Serializable]
    public class UnknownNameAuthenticationException : Exception
    {
        public UnknownNameAuthenticationException() : base()
        {
        }

        public UnknownNameAuthenticationException(string message) : base(message)
        {
        }

        public UnknownNameAuthenticationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnknownNameAuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
