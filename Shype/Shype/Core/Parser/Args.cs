using Shype.Core.Lexer;

namespace Shype.Core.Parser;

public record Args<O>(IImmutableList<Parser<Arg<O>.Setter>> Children)
    : And<Arg<O>.Setter>(Children)
{
    public Object<O> Object(O obj) => new(this, obj);

    public static Args<O> operator &(Arg<O> lhs, Args<O> rhs)
        => new([lhs, .. rhs]);

    public static Args<O> operator &(Args<O> lhs, Arg<O> rhs)
        => new([.. lhs, rhs]);

    public static Args<O> operator &(Args<O> lhs, Args<O> rhs)
        => new([.. lhs, .. rhs]);

    private record WithPrefix(IImmutableList<Parser<Arg<O>.Setter>> Children, Parser Child) : Args<O>(Children)
    {
        public override (State state, IImmutableList<Arg<O>.Setter> result) Apply(State state)
        {
            state = Child.ApplyState(state);
            return base.Apply(state);
        }
    }

    public new Args<O> Prefix(Parser child) => new WithPrefix(Children, child);

    public static Args<O> operator &(string lhs, Args<O> rhs)
        => rhs.Prefix(Token(lhs));

    private record WithSuffix(IImmutableList<Parser<Arg<O>.Setter>> Children, Parser Child) : Args<O>(Children)
    {
        public override (State state, IImmutableList<Arg<O>.Setter> result) Apply(State state)
        {
            (state, IImmutableList<Arg<O>.Setter> result) = base.Apply(state);
            state = Child.ApplyState(state);
            return (state, result);
        }
    }

    public new Args<O> Suffix(Parser child) => new WithSuffix(Children, child);

    public static Args<O> operator &(Args<O> lhs, string rhs)
        => lhs.Suffix(Token(rhs));
}