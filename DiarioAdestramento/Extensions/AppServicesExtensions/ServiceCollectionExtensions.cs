using DiarioAdestramento.Context;
using DiarioAdestramento.Repositories;
using DiarioAdestramento.Repositories.Interfaces;
using DiarioAdestramento.Services;
using Microsoft.EntityFrameworkCore;

namespace DiarioAdestramento.Extensions.AppServicesExtensions;

public static class ServiceCollectionExtensions
{


    public static WebApplicationBuilder AddApiSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        return builder;
    }

    public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        var conectionString = builder.Configuration.GetConnectionString("SqliteConnectionString");
        builder.Services.AddDbContext<AppDbContext>(options =>
              options.UseSqlite(conectionString));
        return builder;
    }

    public static WebApplicationBuilder AddRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddScoped<ILocalRepository, LocalRepository>();
        builder.Services.AddScoped<IRegistroClimaRepository, RegistroClimaRepository>();
        builder.Services.AddScoped<ISessaoTreinoRepository, SessaoTreinoRepository>();
        builder.Services.AddScoped<ICachorroRepository, CachorroRepository>();
        return builder;
    }

    public static WebApplicationBuilder AddExternalServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient<IClimaService, OpenMeteoClimaService>(client =>
        {
            client.BaseAddress = new Uri("https://archive-api.open-meteo.com/");
            client.Timeout = TimeSpan.FromSeconds(10);
        });
        return builder;
    }
    public static WebApplicationBuilder AddControllers(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        return builder;
    }


}
