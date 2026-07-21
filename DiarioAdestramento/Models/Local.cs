using DiarioAdestramento.Enums;

namespace DiarioAdestramento.Models;

public class Local
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public TipoDoLocal? TipoDoLocal { get; set; }
    public string? Obs { get; set; }
    public ICollection<SessaoTreino>? Sessao{ get; set; }
}
