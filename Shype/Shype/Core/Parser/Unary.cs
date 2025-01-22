

namespace Shype.Core.Parser;

public abstract record Unary<Result, ChildResult, ChildParser>(ChildParser Child)
    : Parser<Result>
    where ChildParser : Parser<ChildResult>
{
    public override Lexer.Lexer Lexer() => Child.Lexer();

    protected (State state, ChildResult child_result) ApplyChild(State state)
        => Apply(Child, state);
}

public abstract record Unary<Result, ChildResult>(Parser<ChildResult> Child)
    : Unary<Result, ChildResult, Parser<ChildResult>>(Child);
