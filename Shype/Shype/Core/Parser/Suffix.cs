
namespace Shype.Core.Parser;

public record Suffix<Result>(Parser<Result> Child, Parser Value)
    : Parser<Result>
{
    public override (State state, Result result) Apply(State state)
    {
        (state, Result result) = Try(() => Child.Apply(state));
        state = Try(() => Value.ApplyState(state));
        return (state, result);
    }

    public override Lexer.Lexer Lexer() => Value.Lexer() + Child.Lexer();
}