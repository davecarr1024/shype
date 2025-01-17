namespace Shype.Core.Parser;

public abstract record Transformer<Result, ChildResult>(Parser<ChildResult> Child)
    : UnaryParser<Result, ChildResult>(Child)
{
    protected abstract Result Apply(ChildResult result);

    public override (State state, Result result) Apply(State state)
    {
        (state, ChildResult child_result) = ApplyChild(state);
        return (state, Apply(child_result));
    }

    private record FuncTransformer(Parser<ChildResult> Child, Func<ChildResult, Result> Func)
        : Transformer<Result, ChildResult>(Child)
    {
        protected override Result Apply(ChildResult result) => Func(result);
    }

    public static Transformer<Result, ChildResult> Create(Parser<ChildResult> child, Func<ChildResult, Result> func)
        => new FuncTransformer(child, func);
}