using DiarioAdestramento.Models;
using DiarioAdestramento.Pagination;

namespace DiarioAdestramento.Repositories.Interfaces;

public interface ISessaoTreinoRepository : IRepository<SessaoTreino>
{

   
    Task<SessaoTreino> CriarComClimaAsync(SessaoTreino sessao, Local local);

    
    Task<SessaoTreino?> GetComDetalhesAsync(int id);

    Task<PagedList<SessaoTreino>> GetAllComDetalhesAsync(SessoesParameters parametros);


    Task<PagedList<SessaoTreino>> GetPorCachorroAsync(int cachorroId,int pageNum, int pageSize);


}
