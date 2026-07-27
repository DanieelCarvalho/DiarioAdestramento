namespace DiarioAdestramento.DTOs;

public class DesempenhoPorClimaDTO
{
    public string? Condicao { get; set; }

    public int TotalSessoes { get; set; }

    public int RespostasExcelentes { get; set; }

    public int RespostasBoas { get; set; }

    public int RespostasRegulares { get; set; }
    public int RespostasRuins  { get; set; }

    public double TemperaturaMedia { get; set; }
}
