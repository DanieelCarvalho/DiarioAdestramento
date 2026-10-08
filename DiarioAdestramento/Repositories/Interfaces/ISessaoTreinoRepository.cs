using DiarioAdestramento.Models;
using DiarioAdestramento.Pagination;

namespace DiarioAdestramento.Repositories.Interfaces;

public interface ISessaoTreinoRepository : IRepository<SessaoTreino>
{
    
    Task<SessaoTreino?> GetComDetalhesAsync(int id, string adestradorId);

    Task<PagedList<SessaoTreino>> GetAllComDetalhesAsync(SessoesParameters parametros, string adestradorId);


    Task<PagedList<SessaoTreino>> GetPorCachorroAsync(int cachorroId,
                                                                   string adestradorId,
                                                                   int pageNum,
                                                                   int pageSize);

}
