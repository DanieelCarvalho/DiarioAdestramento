using DiarioAdestramento.Context;
using DiarioAdestramento.Models;
using DiarioAdestramento.Repositories.Interfaces;

namespace DiarioAdestramento.Repositories;

public class CachorroRepository : Repository<Cachorro>, ICachorroRepository
{
    public CachorroRepository(AppDbContext context) : base(context)
    {
    }
}
