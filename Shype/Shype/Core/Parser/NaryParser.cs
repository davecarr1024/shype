

namespace Shype.Core.Parser;

public abstract record NaryParser<Result, ChildResult>(IImmutableList<Parser<ChildResult>> Children)
    : Parser<Result>, IEnumerable<Parser<ChildResult>>
{
    public NaryParser(params Parser<ChildResult>[] children)
        : this(children.ToImmutableList()) { }

    public IEnumerator<Parser<ChildResult>> GetEnumerator() => Children.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Children).GetEnumerator();

    public virtual bool Equals(NaryParser<Result, ChildResult>? rhs)
        => rhs is not null && Children.SequenceEqual(rhs.Children);

    public override int GetHashCode() => Children.GetHashCode();

    protected (State, ChildResult) ApplyChild(Parser<ChildResult> Child, State state)
        => Children.Contains(Child)
            ? Try(() => Child.Apply(state))
            : throw CreateError($"unknown child {Child}");

    public override Lexer.Lexer Lexer()
        => this
            .Select(child => child.Lexer())
            .Aggregate(new(), (Lexer.Lexer lhs, Lexer.Lexer rhs) => lhs + rhs);
}
