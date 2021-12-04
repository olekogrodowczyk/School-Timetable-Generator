using Shared.Responses;
using System;
using System.Net;

namespace UI.Services.Exceptions
{
    public class ApiException : Exception
    {
        public ErrorResult ErrorResult { get; set; }
        public HttpStatusCode StatusCode { get; set; }
 
        public ApiException(ErrorResult error, HttpStatusCode statusCode)
        {
            ErrorResult = error;
            StatusCode = statusCode;
        }

        public ApiException(ErrorResult error)
        {
            ErrorResult = error;
        }
    }
}
