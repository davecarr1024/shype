namespace Shype.Core.Parser;

public abstract record Arg<O> : Parser<Arg<O>.Setter>
{
    public abstract record Setter
    {
        public abstract O Apply(O Obj);
    }

    public static Args<O> operator &(Arg<O> lhs, Arg<O> rhs)
        => new([lhs, rhs]);
}

public record Arg<O, T>(Parser<T> Child, Func<O, T, O> Func)
    : Arg<O>
{
    public override Lexer.Lexer Lexer() => Child.Lexer();

    public new record Setter(Func<O, T, O> Func, T Value) : Arg<O>.Setter
    {
        public override O Apply(O obj) => Func(obj, Value);
    }

    public override (State state, Arg<O>.Setter result) Apply(State state)
    {
        (state, T child_result) = Try(() => Child.Apply(state));
        return (state, new Setter(Func, child_result));
    }
}