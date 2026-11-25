namespace DiarioAdestramento.Exceptions;

public sealed record DetalheErro(string Campo, string Regra);

public abstract class ExcecaoDeAplicacao : Exception
{
    protected ExcecaoDeAplicacao(string erro, 
                                 string mensagem, 
                                 IReadOnlyList<DetalheErro>? detalhes)
        : base(mensagem)
    {
        Erro = erro;
        Detalhes = detalhes ?? [];
    }

    public string Erro { get; }
    public IReadOnlyList<DetalheErro> Detalhes { get; }
    public abstract int StatusCode { get; }
}

public sealed class ValidacaoException(string mensagem, 
                                       IReadOnlyList<DetalheErro>? detalhes = null)
    : ExcecaoDeAplicacao("ValidacaoInvalida", mensagem, detalhes)
{
    public override int StatusCode => StatusCodes.Status400BadRequest;
}

public sealed class NaoEncontradoException(string mensagem)
    : ExcecaoDeAplicacao("NaoEncontrado", mensagem, null)
{
    public override int StatusCode => StatusCodes.Status404NotFound;
}

public sealed class ConflitoException(string mensagem, IReadOnlyList<DetalheErro>? detalhes = null)
    : ExcecaoDeAplicacao("Conflito", mensagem, detalhes)
{
    public override int StatusCode => StatusCodes.Status409Conflict;
}

public sealed class NaoProcessavelException(string mensagem, IReadOnlyList<DetalheErro>? detalhes = null)
    : ExcecaoDeAplicacao("NaoProcessavel", mensagem, detalhes)
{
    public override int StatusCode => StatusCodes.Status422UnprocessableEntity;
}