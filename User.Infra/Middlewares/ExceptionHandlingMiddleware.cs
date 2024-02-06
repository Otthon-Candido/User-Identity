
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using User.Domain.Model;
using User.Infra.Exceptions;

namespace User.Infra.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            ErrorResponse errorResponse = new();

            if (exception is not BaseException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.ResponseText = "Ocorreu um erro inesperado, tente novamente. Caso o problema persista, entre em contato com o suporte.";

                var unknownResult = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(unknownResult);
                return;
            }

            var systemException = exception as BaseException;
            context.Response.StatusCode = (int)systemException.StatusCode;
            errorResponse.ResponseText = exception.Message;
            var result = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(result);
        }
    }
}
