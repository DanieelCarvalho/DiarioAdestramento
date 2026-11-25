using System.Text.Json;
using DiarioAdestramento.Exceptions;

namespace DiarioAdestramento.Models;

public sealed record ErroResponse(string Erro, 
                                  string Mensagem, 
                                  IReadOnlyList<DetalheErro> Detalhes)
{
    public static ErroResponse De(ExcecaoDeAplicacao e) => new(e.Erro, e.Message, e.Detalhes);
}