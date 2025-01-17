namespace Shype.Core.Regex;

public record Or(IImmutableList<Regex> Children) : NaryRegex(Children)
{
    public override string ToString() => base.ToString();

    internal override string ToString(bool first) => ToString(first, "|");

    public override (State state, Result result) Apply(State state)
    {
        List<Errors.Error> errors = [];
        foreach (Regex child in this)
        {
            try
            {
                return child.Apply(state);
            }
            catch (Errors.Error error)
            {
                errors.Add(error);
            }
        }
        throw CreateError("", [.. errors]);
    }

    public static Or operator |(Or lhs, Regex rhs)
        => new([.. lhs, rhs]);

    public static Or operator |(Regex lhs, Or rhs)
        => new([lhs, .. rhs]);

    public static Or operator |(Or lhs, Or rhs)
        => new([.. lhs, .. rhs]);
}
