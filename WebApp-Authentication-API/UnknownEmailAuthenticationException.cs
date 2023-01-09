using System;
using System.Runtime.Serialization;

namespace WebApp_Authentication_API
{
    [Serializable]
    public class UnknownEmailAuthenticationException : Exception
    {
        public UnknownEmailAuthenticationException() : base()
        {
        }

        public UnknownEmailAuthenticationException(string message) : base(message)
        {
        }

        public UnknownEmailAuthenticationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnknownEmailAuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
