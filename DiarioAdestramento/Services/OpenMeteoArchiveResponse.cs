using System.Text.Json.Serialization;

namespace DiarioAdestramento.Services;

// Representa apenas o que precisamos do JSON de resposta do endpoint /v1/archive.
// Ex de resposta real: { "hourly": { "time": [...], "temperature_2m": [...] } }
internal class OpenMeteoArchiveResponse
{
    [JsonPropertyName("hourly")]
    public HourlyData? Hourly { get; set; }
}

internal class HourlyData
{
    [JsonPropertyName("time")]
    public List<string> Time { get; set; } = new();

    [JsonPropertyName("temperature_2m")]
    public List<double?> Temperatura { get; set; } = new();

    [JsonPropertyName("weathercode")]
    public List<int?> WeatherCode { get; set; } = new();

    [JsonPropertyName("precipitation")]
    public List<double?> Precipitacao { get; set; } = new();

    [JsonPropertyName("windspeed_10m")]
    public List<double?> VelocidadeVento { get; set; } = new();
}