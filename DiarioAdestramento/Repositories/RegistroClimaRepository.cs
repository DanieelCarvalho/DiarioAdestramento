using DiarioAdestramento.Context;
using DiarioAdestramento.Models;
using DiarioAdestramento.Repositories.Interfaces;

namespace DiarioAdestramento.Repositories;

public class RegistroClimaRepository : Repository<RegistroClima>, IRegistroClima
{
    public RegistroClimaRepository(AppDbContext context) : base(context)
    {
    }
}
