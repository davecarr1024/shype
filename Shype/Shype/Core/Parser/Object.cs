

namespace Shype.Core.Parser;

public static class Object
{
    public record Builder<O>(IImmutableList<Arg<O>.Setter> Setters)
        : Errors.Errorable<Builder<O>>, IEnumerable<Arg<O>.Setter>
    {
        public O Apply(O obj)
        {
            foreach (Arg<O>.Setter arg in this)
            {
                obj = Try(() => arg.Apply(obj));
            }
            return obj;
        }

        public IEnumerator<Arg<O>.Setter> GetEnumerator() => Setters.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Setters).GetEnumerator();
    }
}

public record Object<O>(Args<O> Args, O Obj)
    : Parser<O>
{
    public override Lexer.Lexer Lexer() => Args.Lexer();

    public override (State state, O result) Apply(State state)
    {
        (state, IImmutableList<Arg<O>.Setter> setters) = Try(() => Args.Apply(state));
        return (state, new Object.Builder<O>(setters).Apply(Obj));
    }
}