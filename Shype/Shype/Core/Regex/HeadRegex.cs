namespace Shype.Core.Regex;

public abstract record HeadRegex : Regex
{
    public abstract Chars.Char Apply(Chars.Char head);

    protected Chars.Char Head(State state) => Try(state.Head, "empty state");

    public override (State state, Result result) Apply(State state)
    {
        Chars.Char head = Head(state);
        return (state.Tail(), new(Try(() => Apply(head), $"error while processing head {head}")));
    }

    public override Not Not() => new(this);
}
