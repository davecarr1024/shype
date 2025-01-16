namespace Shype.Core.Parser;

public abstract record Head<Result> : Parser<Result>
{
    protected abstract Result Apply(Tokens.Token head);

    public override (State state, Result result) Apply(State state)
        => Try(() => (state.Tail(), Apply(state.Head())));
}