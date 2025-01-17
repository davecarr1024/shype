namespace Shype.Core.Regex;



public abstract record Regex : Errors.Errorable<Regex>
{
    public static Regex Create(string input)
        => And([.. input.Select(Literal)]);

    public (State state, Result result) Apply(string input, Chars.Position? position = null)
        => Apply(new(input, position));

    public abstract (State state, Result result) Apply(State state);

    public override string ToString() => ToString(true);

    internal abstract string ToString(bool first);

    public ZeroOrMore ZeroOrMore() => new(this);

    public ZeroOrOne ZeroOrOne() => new(this);

    public OneOrMore OneOrMore() => new(this);

    public static Literal Literal(char value) => new(value);

    public static Any Any() => new();

    public static Range Range(char min, char max) => new(min, max);

    public static Class Class(IImmutableSet<char> values, string? display = null)
        => new(values, display);

    public static Class Whitespace()
        => Class([.. " \t\n"], "\\w");

    public static Class Digits()
        => Class([.. "0123456789"], "\\d");

    public virtual Not Not()
        => throw new NotImplementedException($"non-head regex {this} doesn't support not");

    public static Regex And(params Regex[] children)
        => children.Length switch
        {
            1 => children.First(),
            _ => new And([.. children]),
        };

    public static And operator &(Regex lhs, Regex rhs) => new([lhs, rhs]);

    public static Regex Or(params Regex[] children)
        => children.Length switch
        {
            1 => children.First(),
            _ => new Or([.. children]),
        };

    public static Or operator |(Regex lhs, Regex rhs) => new([lhs, rhs]);
}