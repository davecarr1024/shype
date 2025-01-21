namespace Shype.Core.Parser;

public abstract record Parser : Errors.Errorable<Parser>
{
    public abstract State ApplyState(State state);

    public virtual Lexer.Lexer Lexer() => new();

    protected State ApplyLexer(string input, Chars.Position? position)
        => new(Try(() => Lexer().Apply(input, position), "lex error").Tokens);

    public static Token Token(Lexer.Rule lexRule) => new(lexRule);

    public static Token Token(string name, Regex.Regex value) => Token(new Lexer.Rule(name, value));

    public static Token Token(string name, string value) => Token(name, Regex.Regex.Create(value));

    public static Token Token(string value) => Token(value, value);
}

public abstract record Parser<Result> : Parser
{
    public override State ApplyState(State state)
    {
        (state, _) = Apply(state);
        return state;
    }

    public abstract (State state, Result result) Apply(State state);

    public (State state, Result result) Apply(string input, Chars.Position? position = null)
        => Apply(ApplyLexer(input, position));

    public ZeroOrMore<Result> ZeroOrMore() => new(this);

    public Transform<T, Result> Transform<T>(Func<Result, T> func)
        => Transform<T, Result>.Create(this, func);

    public Prefix<Result> Prefix(Parser value) => new(this, value);

    public Suffix<Result> Suffix(Parser value) => new(this, value);

    public static And<Result> operator &(Parser<Result> lhs, Parser<Result> rhs)
        => new([lhs, rhs]);

    public static Prefix<Result> operator &(string lhs, Parser<Result> rhs)
        => new(rhs, Token(lhs));

    public static Suffix<Result> operator &(Parser<Result> lhs, string rhs)
        => new(lhs, Token(rhs));

    public static Or<Result> operator |(Parser<Result> lhs, Parser<Result> rhs)
        => new([lhs, rhs]);

}