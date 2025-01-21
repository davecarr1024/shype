namespace Shype.Core.Regex;

public record Range(char Min, char Max) : HeadRegex
{
    internal override string ToString(bool first) => $"[{Min}-{Max}]";

    public override Chars.Char Apply(Chars.Char head)
    {
        if (head.Value < Min || head.Value > Max)
        {
            throw CreateError($"expected head in range {Min}-{Max} got {head.Value}");
        }
        return head;
    }
}
