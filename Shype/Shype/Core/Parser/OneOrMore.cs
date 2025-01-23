namespace Shype.Core.Parser;

public record OneOrMore<Result>(Parser<Result> Child)
    : Unary<IImmutableList<Result>, Result>(Child)
{
    public override (State state, IImmutableList<Result> result) Apply(State state)
    {
        (state, Result child_result) = ApplyChild(state);
        List<Result> result = [child_result];
        while (true)
        {
            try
            {
                (state, child_result) = ApplyChild(state);
                result.Add(child_result);
            }
            catch (Errors.Error)
            {
                return (state, result.ToImmutableList());
            }
        }
    }
}