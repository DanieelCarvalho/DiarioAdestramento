using DiarioAdestramento.Context;
using DiarioAdestramento.Models;
using DiarioAdestramento.Repositories.Interfaces;

namespace DiarioAdestramento.Repositories;

public class LocalRepository : Repository<Local>, ILocal
{
    public LocalRepository(AppDbContext context) : base(context)
    {
    }
}
