using System;
using System.Net;

namespace FPSOManagerApi_CS.Utils
{
    public class BusinessException : Exception
    {
        private HttpStatusCode statusCode { get; set; }
        private string message { get; set; }

        public BusinessException()
        {
        }

        public BusinessException(string message) : base(message)
        {
        }

        public BusinessException(string message, Exception inner) : base(message, inner)
        {
        }

        public BusinessException(string message, HttpStatusCode statusCode)
        {
            this.statusCode = statusCode;
            this.message = message;
        }
    }
}