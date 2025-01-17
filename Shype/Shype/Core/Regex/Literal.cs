namespace Shype.Core.Regex;

public record Literal(char Value) : HeadRegex
{
    public override string ToString() => base.ToString();

    internal override string ToString(bool first) => Value.ToString();

    public override Chars.Char Apply(Chars.Char head)
    {
        if (head.Value != Value)
        {
            throw CreateError($"expected literal {Value}");
        }
        return head;
    }
}
