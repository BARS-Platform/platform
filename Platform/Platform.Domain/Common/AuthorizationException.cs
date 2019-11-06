using System;

namespace Platform.Domain.Common
{
    public class AuthorizationException : Exception
    {
        public string ParameterName { get; set; }
        
        public AuthorizationException()
        {
        }

        public AuthorizationException(string message, string parameterName = null) 
            : base (message)
        {
            ParameterName = parameterName;
        }

        public AuthorizationException(string message, Exception innerException)
            :base (message, innerException)
        {
        }
    }
}
