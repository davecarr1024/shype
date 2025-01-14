namespace Shype.Core.Regex;

public record Result(IImmutableList<Chars.Char> Chars) : Chars.Stream<Result>(Chars)
{
    public Result()
        : this(ImmutableList<Chars.Char>.Empty) { }

    public Result(params Chars.Char[] chars)
        : this(chars.ToImmutableList()) { }

    public Result(string input, Chars.Position? position = null)
        : this(FromString(input, position)) { }

    public Tokens.Token AsToken(string type) => new(type, Value, Position);
}
