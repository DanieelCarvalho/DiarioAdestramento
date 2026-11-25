using DiarioAdestramento.Middlewares;
using DiarioAdestramento.Models;
using Microsoft.AspNetCore.Diagnostics;

namespace DiarioAdestramento.Extensions.ExceptionMiddlewareExtensions;

public static class ApiExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
        => app.UseMiddleware<TratamentoDeErroMiddleware>();
}