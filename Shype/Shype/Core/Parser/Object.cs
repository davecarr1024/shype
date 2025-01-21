
namespace Shype.Core.Parser;

public record Object<O>(IImmutableList<Arg<O>> Args)
    : Errors.Errorable<Object<O>>, IEnumerable<Arg<O>>
{
    public O Apply(O obj)
    {
        foreach (Arg<O> arg in this)
        {
            obj = Try(() => arg.Apply(obj));
        }
        return obj;
    }

    public IEnumerator<Arg<O>> GetEnumerator() => Args.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Args).GetEnumerator();
}