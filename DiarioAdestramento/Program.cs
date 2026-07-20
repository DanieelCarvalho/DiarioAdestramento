using DiarioAdestramento.Extensions.ApplicationBuilderExtensions;
using DiarioAdestramento.Extensions.AppServicesExtensions;


var builder = WebApplication.CreateBuilder(args);

builder.AddApiSwagger()
       .AddPersistence()
       .AddRepositories()
       .AddExternalServices()
       .AddControllers();


var app = builder.Build();

app.UseExceptionHandling(app.Environment)
    .UseSwaggerMiddleware()
    .UseAppCors();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
