namespace Shype.Core.Chars;

public record Stream(IImmutableList<Char> Chars)
    : Errors.Errorable<Stream>, IEnumerable<Char>
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

    public virtual bool Equals(Stream? rhs)
        => rhs is not null && Chars.SequenceEqual(rhs.Chars);

    public override int GetHashCode() => Chars.GetHashCode();

    public override string ToString()
        => $"[{string.Join(", ", this.Select(c => c.ToString()))}]";

    public bool Empty() => !this.Any();

    private void AssertNotEmpty()
    {
        if (Empty())
        {
            throw CreateError("empty stream");
        }
    }

    public Char Head()
    {
        AssertNotEmpty();
        return this.First();
    }

    public Stream Tail()
    {
        AssertNotEmpty();
        return this with { Chars = Chars.Skip(1).ToImmutableList() };
    }

    public Position Position() => Head().Position;

    public string Value() => string.Concat(this.Select(c => c.Value));

    public IEnumerator<Char> GetEnumerator() => Chars.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Chars).GetEnumerator();

    public static Stream operator +(Stream lhs, Stream rhs) => new((IImmutableList<Char>)[.. lhs, .. rhs]);

    public static Stream operator +(Stream lhs, Char rhs) => new((IImmutableList<Char>)[.. lhs, rhs]);

    public static Stream operator +(Char lhs, Stream rhs) => new((IImmutableList<Char>)[lhs, .. rhs]);
}