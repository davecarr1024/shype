namespace Shype.Core.Regex;

public abstract record Unary(Regex Child) : Regex
{

    protected (State state, Result result) ApplyChild(State state)
        => Try(() => Child.Apply(state));
}

