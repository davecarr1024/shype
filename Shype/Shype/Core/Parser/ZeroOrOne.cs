namespace Shype.Core.Parser;

public record ZeroOrOne<Result>(Parser<Result> Child)
    : Unary<Result?, Result>(Child)
{
    public override (State state, Result? result) Apply(State state)
    {
        try
        {
            return Child.Apply(state);
        }
        catch (Errors.Error)
        {
            return (state, default(Result?));
        }
    }
}