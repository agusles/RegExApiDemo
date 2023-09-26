using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace RegExAPI.Middleware;

public static class ExceptionMiddleware
{
    public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(err =>
        {
            err.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    // TODO: Logger implementation
                    Console.WriteLine($"Api Error: {contextFeature.Error}");
                    var error = contextFeature.Error;
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(
                        new ApiException(HttpStatusCode.InternalServerError, error.Message),
                        new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        }));
                }
            });
        });
        return app;
    }
}
