namespace DiarioAdestramento.DTOs;

public class CachorroComSessoesDTO
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Raca { get; set; }
    public int? Idade { get; set; }
    public string? Cor { get; set; }
    public string? Observacoes { get; set; }

    public ICollection<SessaoTreinoResumidaDTO>? Sessoes { get; set; }
}
