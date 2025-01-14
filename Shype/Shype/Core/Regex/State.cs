namespace Shype.Core.Regex;

public record State(IImmutableList<Chars.Char> Chars) : Chars.Stream<State>(Chars)
{
    public State()
        : this(ImmutableList<Chars.Char>.Empty) { }

    public State(params Chars.Char[] chars)
        : this(chars.ToImmutableList()) { }

    public State(string input, Chars.Position? position = null)
        : this(FromString(input, position)) { }
}
