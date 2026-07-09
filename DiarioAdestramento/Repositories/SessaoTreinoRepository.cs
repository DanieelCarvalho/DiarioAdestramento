using DiarioAdestramento.Context;
using DiarioAdestramento.Models;
using DiarioAdestramento.Repositories.Interfaces;

namespace DiarioAdestramento.Repositories;

public class SessaoTreinoRepository : Repository<SessaoTreino>, ISessaoTreinoRepository
{
    public SessaoTreinoRepository(AppDbContext context) : base(context)
    {
    }
}
