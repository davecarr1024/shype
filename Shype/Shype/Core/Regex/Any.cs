namespace Shype.Core.Regex;

public record Any : HeadRegex
{
    internal override string ToString(bool first) => ".";

    public override Chars.Char Apply(Chars.Char head) => head;
}
