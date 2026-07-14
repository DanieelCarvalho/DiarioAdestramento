using DiarioAdestramento.Context;
using DiarioAdestramento.Extensions;
using DiarioAdestramento.Repositories;
using DiarioAdestramento.Repositories.Interfaces;
using DiarioAdestramento.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ILocalRepository, LocalRepository>();
builder.Services.AddScoped<IRegistroClimaRepository, RegistroClimaRepository>();
builder.Services.AddScoped<ISessaoTreinoRepository, SessaoTreinoRepository>();
builder.Services.AddScoped<ICachorroRepository, CachorroRepository>();

builder.Services.AddHttpClient<IClimaService, OpenMeteoClimaService>(client =>
{
    client.BaseAddress = new Uri("https://archive-api.open-meteo.com/");
    client.Timeout = TimeSpan.FromSeconds(10);
});

var conectionString = builder.Configuration.GetConnectionString("SqliteConnectionString");


builder.Services.AddDbContext<AppDbContext>(options =>
      options.UseSqlite(conectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ConfigureExceptionHandler();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
