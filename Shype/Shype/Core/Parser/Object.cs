namespace Shype.Core.Parser;

public record Object<O>(Args<O> Args, O Obj)
    : Unary<O, IImmutableList<Arg<O>.Setter>, Args<O>>(Args)
{
    public override (State state, O result) Apply(State state)
    {
        (state, IImmutableList<Arg<O>.Setter> setters) = ApplyChild(state);
        O result = Obj;
        foreach (Arg<O>.Setter setter in setters)
        {
            result = Try(() => setter.Apply(result));
        }
        return (state, result);
    }
}