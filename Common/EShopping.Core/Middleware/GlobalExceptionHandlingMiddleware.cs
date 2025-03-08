using EShopping.Core.Exceptions;
using EShopping.Core.Extensions;
using EShopping.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EShopping.Core.Middleware
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                FailResponseViewModel failResponse = new();

                if (ex.GetType().IsSubclassOf(typeof(BusinessExceptionBase)))
                {
                    failResponse = (ex as BusinessExceptionBase).EmitResult(ex?.Message);
                    response.StatusCode = (int)failResponse.Status.Code;
                }
                else if (ex.GetType().IsSubclassOf(typeof(CustomExceptionBase)))
                {
                    failResponse = (ex as CustomExceptionBase).EmitResult();
                    response.StatusCode = (int)failResponse.Status.Code;
                }
                else
                {
                    response.StatusCode = StatusCodes.Status500InternalServerError;

                    string message = ex.GetExceptionDetails();
                    failResponse = new FailResponseViewModel
                    {
                        Data = null,
                        Status = new StatusViewModel
                        {
                            Message = message,
                            Code = HttpStatusCode.InternalServerError
                        },
                        Exceptions = new string[] { ex.GetType().Name }
                    };
                }

                var serializedResponse = JsonSerializer.Serialize(failResponse);
                _logger.LogError(serializedResponse);
                await context.Response.WriteAsync(serializedResponse);
            }
        }
    }
}
