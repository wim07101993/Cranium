using System.Net;
using System.Net.Http;

namespace Cranium.Data.RestClient.Exceptions
{
    public class Http500Exception : HttpException
    {
        public Http500Exception(string message) : base(message)
        {
        }

        public Http500Exception(HttpStatusCode statusCode, string message) : base(statusCode, message)
        {
        }

        public Http500Exception(HttpResponseMessage response) : base(response)
        {
        }
    }
}
