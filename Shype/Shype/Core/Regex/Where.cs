namespace Shype.Core.Regex;

public record Where(Regex Child, Predicate<Result> Predicate) : Unary(Child)
{
    public override (State state, Result result) Apply(State state)
    {
        (state, Result result) = ApplyChild(state);
        if (!Predicate(result))
        {
            throw CreateError($"result {result} from child {Child} failed predicate {Predicate}");
        }
        return (state, result);
    }

    internal override string ToString(bool first)
        => $"{Child}.Where({Predicate})";
}