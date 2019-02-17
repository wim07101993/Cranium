using System.Net;
using System.Net.Http;

namespace Cranium.Data.RestClient.Exceptions
{
    public class Http400Exception : HttpException
    {
        public Http400Exception(string message) : base(message)
        {
        }

        public Http400Exception(HttpStatusCode statusCode, string message) : base(statusCode, message)
        {
        }

        public Http400Exception(HttpResponseMessage response) : base(response)
        {
        }
    }
}
