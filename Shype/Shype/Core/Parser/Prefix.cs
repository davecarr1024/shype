
namespace Shype.Core.Parser;

public record Prefix<Result>(Parser<Result> Child, Parser Value)
    : Unary<Result, Result>(Child)
{
    public override string ToString() => $"({Value} & {Child})";

    public override (State state, Result result) Apply(State state)
    {
        state = Apply(Value, state);
        return ApplyChild(state);
    }

    public override Lexer.Lexer Lexer() => Value.Lexer() + base.Lexer();
}