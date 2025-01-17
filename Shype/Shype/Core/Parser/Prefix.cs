
namespace Shype.Core.Parser;

public record Prefix<Result>(Parser<Result> Child, Parser Value)
    : Parser<Result>
{
    public override (State state, Result result) Apply(State state)
    {
        state = Try(() => Value.ApplyState(state));
        return Try(() => Child.Apply(state));
    }

    public override Lexer.Lexer Lexer() => Value.Lexer() + Child.Lexer();
}