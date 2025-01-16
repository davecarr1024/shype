namespace Shype.Core.Lexer;

public record Result(Tokens.Stream Tokens)
    : Errors.Errorable<Result>, IEnumerable<Tokens.Token>
{
    public Result(params Tokens.Token[] tokens)
        : this(new Tokens.Stream(tokens.ToImmutableList())) { }

    public IEnumerator<Tokens.Token> GetEnumerator()
        => ((IEnumerable<Tokens.Token>)Tokens).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Tokens).GetEnumerator();

    public static Result operator +(Result lhs, Result rhs)
        => lhs with { Tokens = new((IImmutableList<Tokens.Token>)[.. lhs, .. rhs]) };

    public static Result operator +(Result lhs, Tokens.Token rhs)
        => lhs with { Tokens = new((IImmutableList<Tokens.Token>)[.. lhs, rhs]) };

    public static Result operator +(Tokens.Token lhs, Result rhs)
        => rhs with { Tokens = new((IImmutableList<Tokens.Token>)[lhs, .. rhs]) };
}