namespace Shype.Core.Regex;

public record ZeroOrOne(Regex Child) : UnaryRegex(Child)
{
    internal override string ToString(bool first) => $"{Child.ToString(false)}?";

    public override (State state, Result result) Apply(State state)
    {
        try
        {
            return ApplyChild(state);
        }
        catch (Errors.Error)
        {
            return (state, new());
        }
    }
}
