using System.Text.Json.Serialization;

namespace DiarioAdestramento.Enums;

/// <summary>
/// Indica quão rápido o cão respondeu ao comando.
/// 0 = Excelente, 1 = Bom, 2 = Regular, 3 = Ruim.
/// </summary>
public enum TempoResposta
{
    Excelente,
    Bom,
    Regular,
    Ruim
}
