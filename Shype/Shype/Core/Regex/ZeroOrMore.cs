namespace Shype.Core.Regex;

public record ZeroOrMore(Regex Child) : Unary(Child)
{
    internal override string ToString(bool first) => $"{Child.ToString(false)}*";

    public override (State state, Result result) Apply(State state)
    {
        Result result = new();
        while (true)
        {
            try
            {
                (state, Result child_result) = ApplyChild(state);
                result += child_result;
            }
            catch (Errors.Error)
            {
                return (state, result);
            }
        }
    }
}
