using System.Collections;

namespace Shype.Core.Regex;

public record State(Chars.Stream Chars)
    : Errors.Errorable<State>, IEnumerable<Chars.Char>
{
    public State(params Chars.Char[] chars)
        : this(new Chars.Stream(chars)) { }

    public State(string input, Chars.Position? position = null)
        : this(new Chars.Stream(input, position)) { }

    public IEnumerator<Chars.Char> GetEnumerator() => Chars.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Chars).GetEnumerator();

    public Chars.Char Head() => Try(Chars.Head);

    public State Tail() => this with { Chars = Try(Chars.Tail) };
}
