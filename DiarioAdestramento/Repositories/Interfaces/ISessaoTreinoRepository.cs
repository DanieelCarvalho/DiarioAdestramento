using DiarioAdestramento.Models;

namespace DiarioAdestramento.Repositories.Interfaces;

public interface ISessaoTreinoRepository : IRepository<SessaoTreino>
{

   
    Task<SessaoTreino> CriarComClimaAsync(SessaoTreino sessao, Local local);

    
    Task<SessaoTreino?> GetComDetalhesAsync(int id);

    Task<IEnumerable<SessaoTreino>> GetAllComDetalhesAsync();

    Task<IEnumerable<SessaoTreino>> GetSessoesPorCachorroAsync(int cachorroId);


}
