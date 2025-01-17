namespace Shype.Core.Regex;

public record And(IImmutableList<Regex> Children) : NaryRegex(Children)
{
    internal override string ToString(bool first) => ToString(first, "");

    public override (State state, Result result) Apply(State state)
    {
        Result result = new();
        foreach (Regex child in this)
        {
            (state, Result child_result) = ApplyChild(child, state);
            result += child_result;
        }
        return (state, result);
    }
}
