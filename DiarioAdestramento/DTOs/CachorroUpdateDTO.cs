namespace DiarioAdestramento.DTOs;

public class CachorroUpdateDTO
{
    public int Id { get; set; }
    public int Idade { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Raca { get; set; }
}
