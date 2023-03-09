
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
namespace DaringAPI.Controllers.Errors.MiddleWire
{
    public class ExceptionMiddleWire
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWire> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleWire(
                                 RequestDelegate next, 
                                 ILogger<ExceptionMiddleWire> logger,
                                 IHostEnvironment env)
                                {
                                    this._logger = logger;
                                    this._env = env;
                                    this._next = next;
                                } 
        public async Task InvokeAsync(HttpContext httpContext){
            try{
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                httpContext.Response.ContentType="application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = _env.IsDevelopment()
                ? new ApiExceptions(httpContext.Response.StatusCode, ex.Message, ex.StackTrace?.ToString()):
                  new ApiExceptions(httpContext.Response.StatusCode, "Internal Server Error" );
                  var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
                  var json = JsonSerializer.Serialize(response, options);
                  await httpContext.Response.WriteAsync(json);
            }
            
        }
}
}