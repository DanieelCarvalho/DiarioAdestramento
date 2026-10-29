using DiarioAdestramento.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DiarioAdestramento.Context;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Cachorro> Cachorros { get; set; } = null!;
    public DbSet<Local> Locais { get; set; } = null!;
    public DbSet<SessaoTreino> SessoesTreino { get; set; } = null!;
    public DbSet<RegistroClima> RegistrosClima { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Cachorro>()
    .HasOne<IdentityUser>()
    .WithMany()
    .HasForeignKey(c => c.AdestradorId)
    .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Local>()
            .HasOne<IdentityUser>()
            .WithMany()
            .HasForeignKey(l => l.AdestradorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
