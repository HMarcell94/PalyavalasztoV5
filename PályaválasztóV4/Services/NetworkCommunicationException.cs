using System.Net;

namespace NetWCEx {

    public class NetworkCommunicationException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public NetworkCommunicationException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}