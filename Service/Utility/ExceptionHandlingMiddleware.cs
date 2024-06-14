using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Models;
using System.IO;

namespace Service.Utility
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
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
            _logger.LogError(exception.Message);
            _logger.LogInformation(exception.Message);
            Console.WriteLine(exception.StackTrace);

            context.Response.ContentType = "application/json";
            var response = context.Response;

            var errorResponse = new Result
            {
                success = false
            };
            switch (exception)
            {
                case ApplicationException ex:
                    if (ex.Message.Contains("Invalid Token"))
                    {
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        errorResponse.status = (int)HttpStatusCode.Forbidden;
                        errorResponse.message = ex.Message;
                        
                        break;
                    }
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.status = (int)HttpStatusCode.BadRequest;
                    errorResponse.message = ex.Message;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.message = "Internal server error!";
                    break;
            }
            _logger.LogError(exception.Message);
            _logger.LogInformation(exception.Message);
            
            var result = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(result);
        }

    }
}
