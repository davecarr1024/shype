namespace Shype.Core.Parser;

public abstract record Parser : Errors.Errorable<Parser>
{
    public abstract Lexer.Lexer Lexer();

    protected State ApplyLexer(string input, Chars.Position? position)
        => new(Try(() => Lexer().Apply(input, position), "lex error").Tokens);

    public static Token Token(Lexer.Rule lexRule) => new(lexRule);

    public static Token Token(string name, Regex.Regex value) => Token(new Lexer.Rule(name, value));

    public static Token Token(string name, string value) => Token(name, Regex.Regex.Create(value));

    public static Token Token(string value) => Token(value, value);
}

public abstract record Parser<Result> : Parser
{
    public abstract (State state, Result result) Apply(State state);

    public (State state, Result result) Apply(string input, Chars.Position? position = null)
        => Apply(ApplyLexer(input, position));

    public ZeroOrMore<Result> ZeroOrMore() => new(this);

    public Parser<T> Transform<T>(Func<Result, T> func)
        => Transformer<T, Result>.Create(this, func);

    public static And<Result> operator &(Parser<Result> lhs, Parser<Result> rhs)
        => new([lhs, rhs]);
}