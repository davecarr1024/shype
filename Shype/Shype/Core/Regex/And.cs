namespace Shype.Core.Regex;

public record And(IImmutableList<Regex> Children) : Nary(Children)
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

    public static And operator &(And lhs, Regex rhs)
        => new([.. lhs, rhs]);

    public static And operator &(Regex lhs, And rhs)
        => new([lhs, .. rhs]);

    public static And operator &(And lhs, And rhs)
        => new([.. lhs, .. rhs]);
}
