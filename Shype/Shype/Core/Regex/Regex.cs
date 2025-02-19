namespace Shype.Core.Regex;

public abstract record Regex : Errors.Errorable<Regex>
{
    public static Regex Create(string input)
        // => Parser().Apply(input).result;
        => And([.. input.Select(Literal)]);

    internal static Parser.Parser<Regex> Parser()
        => Core.Regex.Literal.Parser().Transform<Regex>(regex => regex);

    public (State state, Result result) Apply(string input, Chars.Position? position = null)
        => Apply(new(input, position));

    public abstract (State state, Result result) Apply(State state);

    public sealed override string ToString() => ToString(true);

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

    public Where Where(Predicate<Result> predicate)
        => new(this, predicate);

    public Where Except(IImmutableList<string> results)
        => Where(result => !results.Contains(result.Value()));

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

    protected static Class Operators()
        => Class([.. "()[]+*?^-\\"]);
}