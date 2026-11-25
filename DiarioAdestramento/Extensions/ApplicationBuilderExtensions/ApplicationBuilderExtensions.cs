using DiarioAdestramento.Extensions.ExceptionMiddlewareExtensions;
using DiarioAdestramento.Middlewares;

namespace DiarioAdestramento.Extensions.ApplicationBuilderExtensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
        => app.UseMiddleware<TratamentoDeErroMiddleware>();
     public static IApplicationBuilder UseAppCors(this IApplicationBuilder app)
    {
        app.UseCors(p =>
        {
            p.AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader();

        });
        return app;
    }

    public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI( d =>
        {
            d.SwaggerEndpoint("/swagger/v1/swagger.json", 
                              "DiarioAdestramento API v1");
           
        });
        return app;
    }


}
