namespace Shype.Core.Regex;

public record Not(HeadRegex Child) : HeadRegex
{
    public override string ToString() => base.ToString();

    internal override string ToString(bool first) => $"^{Child.ToString(false)}";

    public override (State state, Result result) Apply(State state)
    {
        Chars.Char head = Head(state), child_result;
        try
        {
            child_result = Child.Apply(head);
        }
        catch (Errors.Error)
        {
            return (state.Tail(), new(head));
        }
        throw CreateError($"child {Child} expected to not match but matched {child_result}");
    }

    public override Chars.Char Apply(Chars.Char head) => throw new NotImplementedException();
}
