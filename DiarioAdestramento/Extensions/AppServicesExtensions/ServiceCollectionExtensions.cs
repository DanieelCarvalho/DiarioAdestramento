using DiarioAdestramento.Context;
using DiarioAdestramento.Repositories;
using DiarioAdestramento.Repositories.Interfaces;
using DiarioAdestramento.Services;
using DiarioAdestramento.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace DiarioAdestramento.Extensions.AppServicesExtensions;

public static class ServiceCollectionExtensions
{


    public static WebApplicationBuilder AddApiSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "Token",
                In = ParameterLocation.Header,
                Description = "Token Access",

            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type= ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Diário de Adestramento API",
                Version = "v1",
                Description = "API para gerenciamento do diário de adestramento de cães.",
                Contact = new OpenApiContact
                {
                    Name = "Daniel Carvalho",
                    Email = "danielcarvalhocode@gmail.com",
                    Url = new Uri("https://www.linkedin.com/in/daniel-carvalho-dev/")
                }
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        return builder;
    }

    public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        var conectionString = builder.Configuration.GetConnectionString("SqliteConnectionString");
        builder.Services.AddDbContext<AppDbContext>(options =>
              options.UseSqlite(conectionString));

        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                        .AddEntityFrameworkStores<AppDbContext>()
                        .AddDefaultTokenProviders();

        return builder;
    }

    public static WebApplicationBuilder AddRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddScoped<ILocalRepository, LocalRepository>();
        builder.Services.AddScoped<IRegistroClimaRepository, RegistroClimaRepository>();
        builder.Services.AddScoped<ISessaoTreinoRepository, SessaoTreinoRepository>();
        builder.Services.AddScoped<ICachorroRepository, CachorroRepository>();
        builder.Services.AddScoped<ICachorroService, CachorroService>();
        builder.Services.AddScoped<ILocalService, LocalService>();
        builder.Services.AddScoped<ISessaoTreinoService, SessaoTreinoService>();
        builder.Services.AddScoped<IEstatisticaRepository,EstatisticaRepository>();
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
        builder.Services.AddAuthentication()
                        .AddBearerToken();


        builder.Services.AddAuthorization();
        return builder;
    }


}
