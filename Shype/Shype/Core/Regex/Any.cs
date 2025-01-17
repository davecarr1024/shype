namespace Shype.Core.Regex;

public record Any : HeadRegex
{
    public override string ToString() => base.ToString();

    internal override string ToString(bool first) => ".";

    public override Chars.Char Apply(Chars.Char head) => head;
}
