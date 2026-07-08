namespace DiarioAdestramento.Models;

public class Local
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public ICollection<SessaoTreino>? Sessao{ get; set; }
}
