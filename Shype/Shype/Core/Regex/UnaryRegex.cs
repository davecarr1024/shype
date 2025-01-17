namespace Shype.Core.Regex;

public abstract record UnaryRegex(Regex Child) : Regex
{
    public override string ToString() => base.ToString();

    protected (State state, Result result) ApplyChild(State state)
        => Try(() => Child.Apply(state));
}

