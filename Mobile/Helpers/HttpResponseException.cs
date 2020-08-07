using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;

namespace Mobile.Helpers
{
    class HttpResponseException : Exception
    {
        public HttpResponseMessage Response { get; private set; }


        public HttpResponseException(HttpResponseMessage response) :base(response.ReasonPhrase)
        {
            Response = response;
        }

        public HttpResponseException(Exception innerException, HttpResponseMessage response): base(response.ReasonPhrase, innerException)
        {
            Response = response;
        }
    }
}
