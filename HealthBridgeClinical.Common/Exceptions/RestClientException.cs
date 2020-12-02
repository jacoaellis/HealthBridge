using System;

namespace HealthBridgeClinical.Common.Exceptions
{
    public class RestClientException : Exception
    {
        public RestClientException(string message, Exception innerException) : base(message, innerException)
        { }

        public RestClientException(string message) : base(message)
        { }

        public RestClientException()
        { }
    }
}
