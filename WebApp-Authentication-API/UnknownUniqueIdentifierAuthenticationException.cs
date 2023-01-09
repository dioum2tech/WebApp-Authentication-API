using System;
using System.Runtime.Serialization;

namespace WebApp_Authentication_API
{
    [Serializable]
    public class UnknownUniqueIdentifierAuthenticationException : Exception
    {
        public UnknownUniqueIdentifierAuthenticationException() : base()
        {
        }

        public UnknownUniqueIdentifierAuthenticationException(string message) : base(message)
        {
        }

        public UnknownUniqueIdentifierAuthenticationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnknownUniqueIdentifierAuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
