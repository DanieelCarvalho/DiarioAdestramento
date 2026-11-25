using System.Text.Json;
using DiarioAdestramento.Exceptions;
using DiarioAdestramento.Models;

namespace DiarioAdestramento.Middlewares;

public class TratamentoDeErroMiddleware(RequestDelegate proximo, ILogger<TratamentoDeErroMiddleware> logger)
{
    private static readonly JsonSerializerOptions Opcoes = new(JsonSerializerDefaults.Web);

    public async Task InvokeAsync(HttpContext contexto)
    {
        try
        {
            await proximo(contexto);
        }
        catch (ExcecaoDeAplicacao excecao)
        {
            logger.LogWarning("Requisição recusada. Rota: {Rota}. Erro: {Erro}. Mensagem: {Mensagem}",
                contexto.Request.Path, excecao.Erro, excecao.Message);

            await EscreverAsync(contexto, excecao.StatusCode, ErroResponse.De(excecao));
        }
        catch (Exception excecao)
        {
            logger.LogError(excecao, "Falha não tratada. Rota: {Rota}. Método: {Metodo}",
                contexto.Request.Path, contexto.Request.Method);

            await EscreverAsync(contexto, StatusCodes.Status500InternalServerError,
                new ErroResponse("ErroInterno", "Erro interno ao processar a requisição", []));
        }
    }

    private static async Task EscreverAsync(HttpContext contexto, int statusCode, ErroResponse resposta)
    {
        if (contexto.Response.HasStarted) return;

        contexto.Response.Clear();
        contexto.Response.StatusCode = statusCode;
        contexto.Response.ContentType = "application/json; charset=utf-8";
        await contexto.Response.WriteAsync(JsonSerializer.Serialize(resposta, Opcoes));
    }
}