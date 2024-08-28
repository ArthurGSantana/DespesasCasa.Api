using System.Diagnostics;
using System.Net;
using DespesasCasa.Domain.Enum;
using DespesasCasa.Domain.Exceptions;
using DespesasCasa.Domain.Extensions;
using DespesasCasa.Domain.Model.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DespesasCasa.Api.Filters;

public class HttpGlobalExceptionFilter(IWebHostEnvironment _env, ILogger<HttpGlobalExceptionFilter> _logger) : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, $"TraceId: {Activity.Current?.Context.TraceId} - " + context.Exception.Message);

        var error = new Error(
            Enum.GetName(ErrorCodeEnum.InternalServerError)?.ToSnakeCase()?.ToLower() ?? "", $"{context.Exception}");

        HttpResponse response = context.HttpContext.Response;
        ErrorViewModel errorsViewModel = new ErrorViewModel(error);

        if (_env.IsProduction())
        {
            error.Message = "An internal error occurred. Please, try again later.";
        }

        response.StatusCode = (int)HttpStatusCode.InternalServerError;

        if (context.Exception is AppDomainException exception)
        {
            response.StatusCode = (int)HttpStatusCode.BadRequest;

            error.Code = Enum.GetName(exception.ErrorCode)?.ToSnakeCase()?.ToLower() ?? "";
            error.Message = exception.Message;
            if (exception.StatusCode.HasValue)
            {
                response.StatusCode = (int)exception.StatusCode;
            }
        }

        response.ContentType = "application/json";

        context.ExceptionHandled = true;
        context.Result = new ObjectResult(errorsViewModel);
    }
}
