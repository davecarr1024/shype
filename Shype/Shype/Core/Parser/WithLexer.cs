namespace Shype.Core.Parser;

public record WithLexer<Result>(Parser<Result> Child, Lexer.Lexer Value)
    : Unary<Result, Result>(Child)
{
    public override (State state, Result result) Apply(State state)
        => ApplyChild(state);

    public override Lexer.Lexer Lexer() => Value + base.Lexer();
}