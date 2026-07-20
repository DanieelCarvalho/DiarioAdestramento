using DiarioAdestramento.Models;
using Microsoft.AspNetCore.Diagnostics;

namespace DiarioAdestramento.Extensions.ExceptionMiddlewareExtensions;

public static class ApiExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(error =>
        {
            error.Run(async context =>
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                if (contextFeature is not null)
                {
                    await context.Response.WriteAsync(new ErrorDetails()
                    {
                        StatusCode = context.Response.StatusCode.ToString(),
                        Message =  "Ocorreu um erro interno no servidor.",
                        Trace = contextFeature.Error.StackTrace
                    }.ToString());
                }
            });
        });
    }
}
