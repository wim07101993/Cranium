using System;
using System.Net;
using System.Net.Http;

namespace Cranium.Data.RestClient.Exceptions
{
    public class HttpException : Exception
    {
        public HttpException(string message)
        {
            Message = message;
        }

        public HttpException(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public HttpException(HttpResponseMessage response)
        {
            HttpResponseMessage = response;
            StatusCode = response.StatusCode;
            var message = HttpResponseMessage.Content.ReadAsStringAsync();
            message.RunSynchronously();
            Message = message.Result;
        }

        public HttpResponseMessage HttpResponseMessage { get; set; }
        public HttpStatusCode StatusCode { get; }
        public override string Message { get; }
    }
}