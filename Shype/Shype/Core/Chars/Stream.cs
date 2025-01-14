namespace Shype.Core.Chars;

public abstract record Stream<S>(IImmutableList<Char> Chars)
    : Streams.Stream<S, Char>(Chars)
    where S : Stream<S>, new()
{
    public Stream() : this(ImmutableList<Char>.Empty) { }

    public Stream(params Char[] chars) : this(chars.ToImmutableList()) { }

    public Stream(string input, Position? position = null)
        : this(FromString(input, position ?? new(0, 0))) { }

    protected static ImmutableList<Char> FromString(string input, Position? position)
    {
        List<Char> chars = [];
        Position pos = position ?? new(0, 0);
        foreach (char c in input)
        {
            chars.Add(new(c, pos));
            if (c == '\n')
            {
                pos = new(pos.Line + 1, 0);
            }
            else
            {
                pos = new(pos.Line, pos.Column + 1);
            }
        }
        return [.. chars];
    }

    public Position Position { get => Head.Position; }

    public string Value { get => string.Join(null, this.Select(c => c.Value)); }
}