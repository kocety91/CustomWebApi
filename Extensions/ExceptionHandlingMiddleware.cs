using CustomWebApi.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace CustomWebApi.Extensions
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";

            httpContext.Response.StatusCode = exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            var errorDetails = new ErrorDetails()
            {
                Message = exception.Message,
                StatusCode = httpContext.Response.StatusCode
            };

            await httpContext.Response.WriteAsync(errorDetails.ToString());
        }
    }
}
