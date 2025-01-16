namespace Shype.Core.Parser;

public abstract record Parser<Result> : Errors.Errorable<Parser<Result>>
{
    public abstract (State state, Result result) Apply(State state);

    public (State state, Result result) Apply(string input, Chars.Position? position = null)
        => Apply(ApplyLexer(input, position));

    protected State ApplyLexer(string input, Chars.Position? position)
        => new(Try(() => Lexer().Apply(input, position), "lex error").Tokens);

    public abstract Lexer.Lexer Lexer();

    public ZeroOrMore<Result> ZeroOrMore() => new(this);

    public Parser<T> Transform<T>(Func<Result, T> func)
        => Transformer<T, Result>.Create(this, func);
}