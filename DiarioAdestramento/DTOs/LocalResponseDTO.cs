using DiarioAdestramento.Models;

namespace DiarioAdestramento.DTOs;

public class LocalResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public ICollection<SessaoTreino>? Sessao { get; set; }
}
