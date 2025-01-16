namespace Shype.Core.Chars;

public record Stream(IImmutableList<Char> Items)
    : Streams.Stream<Stream, Char>(Items)
{
    public Stream(params Char[] chars) : this(chars.ToImmutableList()) { }

    public Stream(string input, Position? position = null)
        : this(FromString(input, position)) { }

    private static IImmutableList<Char> FromString(string input, Position? position)
    {
        List<Char> chars = [];
        Position pos = position ?? new(0, 0);
        foreach (char c in input)
        {
            chars.Add(new(c, pos));
            pos = c == '\n' ? new(pos.Line + 1, 0) : new(pos.Line, pos.Column + 1);
        }
        return [.. chars];
    }

    public override string ToString() => base.ToString();

    public Position Position() => Head().Position;

    public string Value() => string.Concat(this.Select(c => c.Value));
}