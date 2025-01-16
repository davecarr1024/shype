namespace Shype.Core.Parser;

public record State(Tokens.Stream Tokens)
    : Errors.Errorable<State>, IEnumerable<Tokens.Token>
{
    public State(params Tokens.Token[] tokens)
        : this(new Tokens.Stream(tokens.ToImmutableList())) { }

    public virtual bool Equals(State? rhs) => rhs is not null && Tokens.SequenceEqual(rhs.Tokens);

    public override int GetHashCode() => Tokens.GetHashCode();

    public IEnumerator<Tokens.Token> GetEnumerator() => ((IEnumerable<Tokens.Token>)Tokens).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Tokens).GetEnumerator();

    public Tokens.Token Head() => Try(Tokens.Head);

    public State Tail() => this with { Tokens = Try(Tokens.Tail) };
}