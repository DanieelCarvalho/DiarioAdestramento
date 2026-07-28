using DiarioAdestramento.Enums;

namespace DiarioAdestramento.DTOs.Estatisticas;

public class DesempenhoPorLocalDTO
{
    public string NomeLocal { get; set; } = string.Empty;
    public int TotalSessoes { get; set; }
    public double PercentualExcelente { get; set; }
    public double PercentualBom { get; set; }
    public double PercentualRegular { get; set; }
    public double PercentualRuim { get; set; }
    public TipoDoLocal? TipoDoLocal { get; set; }




}