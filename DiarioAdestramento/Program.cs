using DiarioAdestramento.Context;
using DiarioAdestramento.Repositories;
using DiarioAdestramento.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ILocal, LocalRepository>();
builder.Services.AddScoped<IRegistroClima, RegistroClimaRepository>();
builder.Services.AddScoped<ISessaoTreinoRepository, SessaoTreinoRepository>();
builder.Services.AddScoped<ICachorroRepository, CachorroRepository>();

var conectionString = builder.Configuration.GetConnectionString("SqliteConnectionString");


builder.Services.AddDbContext<AppDbContext>(options =>
      options.UseSqlite(conectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
