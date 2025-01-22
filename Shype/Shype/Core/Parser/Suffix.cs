
namespace Shype.Core.Parser;

public record Suffix<Result>(Parser<Result> Child, Parser Value)
    : Unary<Result, Result>(Child)
{
    public override string ToString() => $"({Child} & {Value})";

    public override (State state, Result result) Apply(State state)
    {
        (state, Result result) = ApplyChild(state);
        state = Apply(Value, state);
        return (state, result);
    }

    public override Lexer.Lexer Lexer() => Value.Lexer() + base.Lexer();
}