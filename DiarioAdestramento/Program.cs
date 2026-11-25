using DiarioAdestramento.Extensions.ApplicationBuilderExtensions;
using DiarioAdestramento.Extensions.AppServicesExtensions;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

builder.AddApiSwagger()
       .AddControllers()
       .AddPersistence()
       .AddRepositories()
       .AddExternalServices();


var app = builder.Build();

app.UseExceptionHandling()
    .UseSwaggerMiddleware()
    .UseAppCors();


app.UseHttpsRedirection();

app.UseAuthorization();
app.MapIdentityApi<IdentityUser>();
app.MapControllers();

app.Run();
