namespace Shype.Core.Regex;

public record OneOrMore(Regex Child) : UnaryRegex(Child)
{
    internal override string ToString(bool first) => $"{Child.ToString(false)}+";

    public override (State state, Result result) Apply(State state)
    {
        Result result = new();
        (state, Result child_result) = ApplyChild(state);
        result += child_result;
        while (true)
        {
            try
            {
                (state, child_result) = ApplyChild(state);
                result += child_result;
            }
            catch (Errors.Error)
            {
                return (state, result);
            }
        }
    }
}
