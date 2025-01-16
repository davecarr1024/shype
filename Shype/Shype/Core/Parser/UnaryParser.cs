
namespace Shype.Core.Parser;

public abstract record UnaryParser<Result, ChildResult>(Parser<ChildResult> Child) : Parser<Result>
{
    protected (State state, ChildResult result) ApplyChild(State state) => Try(() => Child.Apply(state));

    public override Lexer.Lexer Lexer() => Child.Lexer();
}