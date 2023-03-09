using System.Net;
using FluentValidation;
using FluentValidation.Results;
using GPTOverflow.API.Modules.CrossCuttingConcerns.Models;
using GPTOverflow.Core.CrossCuttingConcerns.Exceptions;
using GPTOverflow.Core.CrossCuttingConcerns.Utils;
using Microsoft.EntityFrameworkCore;

namespace GPTOverflow.API.Modules.CrossCuttingConcerns.Middlewares;

public class ExceptionFormattingMiddleware : IFactoryMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<ExceptionFormattingMiddleware> _logger;

    public ExceptionFormattingMiddleware(RequestDelegate next, IWebHostEnvironment env,
        ILoggerFactory loggerFactory)
    {
        _next = next;
        _env = env;
        _logger = loggerFactory
            .CreateLogger<ExceptionFormattingMiddleware>();
    }

    private Task HandleException(HttpContext context, Exception exception, IWebHostEnvironment env)
    {
        try
        {
            var code = HttpStatusCode.InternalServerError;
            var error = env.IsDevelopment()
                ? exception.Message
                : DefaultErrorMessages.ServerError;
            var errors = new List<ValidationFailure>();
            
            switch (exception)
            {
                case DbUpdateException dbUpdateException:
                    code = HttpStatusCode.BadRequest;
                    error = env.IsDevelopment()
                        ? (dbUpdateException.InnerException?.Message ?? dbUpdateException.Message)
                        : DefaultErrorMessages.ConstraintViolation;
                    break;
                case DomainValidationException domainValidationException:
                    code = HttpStatusCode.UnprocessableEntity;
                    error = domainValidationException.Message;
                    errors = domainValidationException.Errors;
                    break;
                case BusinessRuleViolationException businessRuleViolationException:
                    code = HttpStatusCode.BadRequest;
                    error = businessRuleViolationException.Message;
                    break;
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    error = validationException.Message;
                    break;
            }

            if (code == HttpStatusCode.InternalServerError)
            {
                _logger.LogError(exception, message:
                    "Http Request Exception Information: {Environment} Schema:{Schema} Host: {Host} Path: {Path} QueryString: {QueryString}  Error Message: {ErrorMessage} Error Trace: {StackTrace}, ToString: {ToString}",
                    Environment.NewLine, context.Request.Scheme, context.Request.Host, context.Request.Path,
                    context.Request.QueryString, exception.Message, exception.StackTrace, exception.ToString());
            }

            var response = Envelope.Error(error,errors);
            var result = AppJsonUtils.Serialize(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception in middleware task");

            var response = Envelope.Error("Something went wrong");
            var result = AppJsonUtils.Serialize(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(result);
        }
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex, _env);
        }
    }
}