namespace Shype.Core.Parser;

public record And<Result>(IImmutableList<Parser<Result>> Children)
    : Nary<IImmutableList<Result>, Result>(Children)
{
    public override string ToString()
        => $"({string.Join(" & ", from child in this select child.ToString())})";

    public override (State state, IImmutableList<Result> result) Apply(State state)
    {
        List<Result> result = [];
        foreach (Parser<Result> child in this)
        {
            (state, Result child_result) = ApplyChild(child, state);
            result.Add(child_result);
        }
        return (state, result.ToImmutableList());
    }

    public static And<Result> operator &(And<Result> lhs, Parser<Result> rhs)
        => new([.. lhs, rhs]);

    public static And<Result> operator &(Parser<Result> lhs, And<Result> rhs)
        => new([lhs, .. rhs]);

    public static And<Result> operator &(And<Result> lhs, And<Result> rhs)
        => new([.. lhs, .. rhs]);

}