using System.Globalization;
using System.Net.Http.Json;
using DiarioAdestramento.DTOs;
using DiarioAdestramento.Models;
using Microsoft.Extensions.Logging;

namespace DiarioAdestramento.Services;

public class OpenMeteoClimaService : IClimaService
{
    private readonly HttpClient _http;
    private readonly ILogger<OpenMeteoClimaService> _logger;

    public OpenMeteoClimaService(HttpClient http, ILogger<OpenMeteoClimaService> logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task<ClimaResultadoDto?> ObterClimaHistoricoAsync(double latitude, double longitude, DateTime dataHora)
    {
        var data = dataHora.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

        // start_date e end_date iguais: só queremos as horas de um único dia
        var url = "v1/archive"
            + $"?latitude={latitude.ToString(CultureInfo.InvariantCulture)}"
            + $"&longitude={longitude.ToString(CultureInfo.InvariantCulture)}"
            + $"&start_date={data}&end_date={data}"
            + "&hourly=temperature_2m,weathercode,precipitation,windspeed_10m"
            + "&timezone=America%2FSao_Paulo";

        try
        {
            var resposta = await _http.GetFromJsonAsync<OpenMeteoArchiveResponse>(url);

            if (resposta?.Hourly is null || resposta.Hourly.Time.Count == 0)
            {
                _logger.LogWarning(
                    "Open-Meteo não retornou dados horários para {Data} em {Lat},{Lon}",
                    data, latitude, longitude);
                return null;
            }

            // A API devolve uma lista com as 24 horas do dia (ex: "2026-07-09T10:00").
            // Precisamos achar o índice da hora que bate com o horário informado.
            var horaAlvo = new DateTime(dataHora.Year, dataHora.Month, dataHora.Day, dataHora.Hour, 0, 0);

            var indice = resposta.Hourly.Time.FindIndex(t =>
                DateTime.TryParse(t, CultureInfo.InvariantCulture, DateTimeStyles.None, out var horaLista)
                && horaLista == horaAlvo);

            if (indice == -1)
            {
                _logger.LogWarning("Horário {HoraAlvo} não encontrado na resposta do Open-Meteo", horaAlvo);
                return null;
            }

            return new ClimaResultadoDto
            {
                TemperaturaCelsius = resposta.Hourly.Temperatura.ElementAtOrDefault(indice) ?? 0,
                CondicaoTempo = MapearCondicao(resposta.Hourly.WeatherCode.ElementAtOrDefault(indice)),
                Precipitacao = resposta.Hourly.Precipitacao.ElementAtOrDefault(indice),
                VelocidadeVento = resposta.Hourly.VelocidadeVento.ElementAtOrDefault(indice)
            };
        }
        catch (HttpRequestException ex)
        {
            // Não deixamos uma falha na API externa derrubar o salvamento da sessão de treino.
            // Quem chamar este método recebe null e decide como tratar (ex: salvar sessão sem clima).
            _logger.LogError(
                ex, "Falha ao consultar Open-Meteo para {Lat},{Lon} em {DataHora}",
                latitude, longitude, dataHora);
            return null;
        }
    }

    // Tradução simplificada dos WMO Weather Codes usados pelo Open-Meteo.
    // Referência completa: https://open-meteo.com/en/docs (seção weathercode)
    private static string MapearCondicao(int? codigo)
    {
        return codigo switch
        {
            0 => "Céu limpo",
            1 or 2 or 3 => "Parcialmente nublado",
            45 or 48 => "Nevoeiro",
            51 or 53 or 55 or 56 or 57 => "Garoa",
            61 or 63 or 65 or 66 or 67 => "Chuva",
            71 or 73 or 75 or 77 => "Neve",
            80 or 81 or 82 => "Pancadas de chuva",
            95 or 96 or 99 => "Tempestade",
            _ => "Não identificado"
        };
    }
}