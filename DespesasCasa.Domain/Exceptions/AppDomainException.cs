using System;
using System.Net;
using DespesasCasa.Domain.Enum;

namespace DespesasCasa.Domain.Exceptions;

public class AppDomainException : Exception
{
    public ErrorCodeEnum ErrorCode { get; set; }
    public HttpStatusCode? StatusCode { get; set; }

    public AppDomainException(string message, ErrorCodeEnum errorCode, HttpStatusCode? statusCode = null)
        : base(message)
    {
        this.ErrorCode = errorCode;
        this.StatusCode = statusCode;
    }

    public AppDomainException(string message, ErrorCodeEnum errorCode, Exception innerException, HttpStatusCode? statusCode = null)
        : base(message, innerException)
    {
        this.ErrorCode = errorCode;
        this.StatusCode = statusCode;
    }
}
