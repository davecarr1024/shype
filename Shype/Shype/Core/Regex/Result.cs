
namespace Shype.Core.Regex;

public record Result(Chars.Stream Chars)
    : Errors.Errorable<Result>, IEnumerable<Chars.Char>
{
    public Result(params Chars.Char[] chars) 
        : this(new Chars.Stream(chars)) { }

    public Result(string input, Chars.Position? position = null) 
        : this(new Chars.Stream(input, position)) { }

    public string Value() => Chars.Value();

    public Chars.Position Position() => Try(Chars.Position);

    public IEnumerator<Chars.Char> GetEnumerator() => ((IEnumerable<Chars.Char>)Chars).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Chars).GetEnumerator();

    public static Result operator +(Result lhs, Result rhs)
        => lhs with { Chars = new((IImmutableList<Chars.Char>)[.. lhs, .. rhs]) };

    public static Result operator +(Result lhs, Chars.Char rhs)
        => lhs with { Chars = new((IImmutableList<Chars.Char>)[.. lhs, rhs]) };

    public static Result operator +(Chars.Char lhs, Result rhs)
        => rhs with { Chars = new((IImmutableList<Chars.Char>)[lhs, .. rhs]) };
}
