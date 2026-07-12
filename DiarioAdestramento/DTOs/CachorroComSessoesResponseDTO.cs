namespace DiarioAdestramento.DTOs;

public class CachorroComSessoesResponseDTO
{
    public int Id { get; set; }
    public int Idade { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Raca { get; set; }
    public List<SessaoResumoDTO> Sessoes { get; set; } = new();
}