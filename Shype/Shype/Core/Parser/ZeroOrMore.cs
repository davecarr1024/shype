namespace Shype.Core.Parser;

public record ZeroOrMore<Result>(Parser<Result> Child)
    : UnaryParser<IImmutableList<Result>, Result>(Child)
{
    public override (State state, IImmutableList<Result> result) Apply(State state)
    {
        List<Result> result = [];
        while (true)
        {
            try
            {
                (state, Result child_result) = ApplyChild(state);
                result.Add(child_result);
            }
            catch (Errors.Error)
            {
                return (state, result.ToImmutableList());
            }
        }
    }
}