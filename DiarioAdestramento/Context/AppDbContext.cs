using DiarioAdestramento.Models;
using Microsoft.EntityFrameworkCore;

namespace DiarioAdestramento.Context;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Cachorro> Cachorros { get; set; } = null!;
    public DbSet<Local> Locais { get; set; } = null!;
    public DbSet<SessaoTreino> SessoesTreino { get; set; } = null!;
    public DbSet<RegistroClima> RegistrosClima { get; set; } = null!;


}
