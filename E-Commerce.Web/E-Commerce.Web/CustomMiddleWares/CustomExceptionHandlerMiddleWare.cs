using DomainLayer.Exceptions;
using E_Commerce.Web.Models;
using Shared.ErrorModels;
using System.Net;
using System.Text.Json;

namespace E_Commerce.Web.CustomMiddleWares
{
    // have a Public Constructor(Take RequestDelegate as a parameter )
    // Have a method called Invoke(Return Task , Take HttpContext as a parameter)
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> _logger;

        public CustomExceptionHandlerMiddleWare(RequestDelegate Next , ILogger<CustomExceptionHandlerMiddleWare> logger)
        {
            _next = Next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
                await HandleNotFoundEndPointAsync(httpContext);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "something went wrong");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            var Response = new ErrorToReturn();
            //set status code for response
            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                BadRequestException badRequestException => GetBadRequestStatusCodeInsertErrors( badRequestException, Response),
                _ => StatusCodes.Status500InternalServerError,
            };


            //set content type for response 
            //follow to Way01
            //httpContext.Response.ContentType = "application/json";


            //response object 
            Response.StatusCode = httpContext.Response.StatusCode;
            Response.ErrorMessage = ex.Message;
            

            //return -> response object AS json
            //Way01
            //var ResponseToReturn = JsonSerializer.Serialize(Response);
            //await httpContext.Response.WriteAsync(ResponseToReturn);
            //Way02
            await httpContext.Response.WriteAsJsonAsync(Response);
        }

        private static int GetBadRequestStatusCodeInsertErrors(BadRequestException badRequestException , ErrorToReturn Response)
        {
            Response.Errors=badRequestException.Errors;
            return StatusCodes.Status400BadRequest;
        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                var Response = new ErrorToReturn()
                {
                    StatusCode = httpContext.Response.StatusCode,
                    ErrorMessage = $"End Point {httpContext.Request.Path} Is Not Found"
                };
                await httpContext.Response.WriteAsJsonAsync(Response);
            }
        }
    }
}
