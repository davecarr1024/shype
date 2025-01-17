

namespace Shype.Core.Parser;

public abstract record UnaryParser<Result, ChildResult>(Parser<ChildResult> Child)
    : Parser<Result>
{
    public override Lexer.Lexer Lexer() => Child.Lexer();

    protected (State state, ChildResult child_result) ApplyChild(State state)
        => Try(() => Child.Apply(state));
}