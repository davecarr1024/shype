namespace Shype.Core.Regex;

public record Class(IImmutableSet<char> Values, string? Display) : HeadRegex
{
    public override string ToString() => base.ToString();

    internal override string ToString(bool first) => Display ?? $"({string.Concat(Values)})";

    public override Chars.Char Apply(Chars.Char head)
        => Values.Contains(head.Value) ? head : throw CreateError($"expected head in set {Values} got {head.Value}");
}
