namespace Shype.Core.Parser;

public record Or<Result>(IImmutableList<Parser<Result>> Children)
    : Nary<Result, Result>(Children)
{
    public override string ToString()
        => $"({string.Join(" | ", from child in this select child.ToString())})";

    public override (State state, Result result) Apply(State state)
    {
        List<Errors.Error> errors = [];
        foreach (Parser<Result> child in this)
        {
            try
            {
                return ApplyChild(child, state);
            }
            catch (Errors.Error error)
            {
                errors.Add(error);
            }
        }
        throw CreateError("", [.. errors]);
    }

    public static Or<Result> operator |(Or<Result> lhs, Parser<Result> rhs)
        => new([.. lhs, rhs]);

    public static Or<Result> operator |(Parser<Result> lhs, Or<Result> rhs)
        => new([lhs, .. rhs]);
}