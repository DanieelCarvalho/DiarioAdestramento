using DiarioAdestramento.Models;

namespace DiarioAdestramento.Repositories.Interfaces;

public interface ICachorroRepository : IRepository<Cachorro>
{
    Task<Cachorro?> GetComSessoesAsync(int id);
}
