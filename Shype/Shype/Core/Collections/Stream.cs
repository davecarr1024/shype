using Shype.Core.Errors;

namespace Shype.Core.Collections;

public abstract record Stream<S, T> : List<T> where S : Stream<S, T>
{
    public Stream(params T[] items) : base(items) { }

    public T Head
    {
        get
        {
            if (!this.Any())
            {
                throw new Error("getting head from empty stream");
            }
            return this.First();
        }
    }

    public Stream<S, T> Tail
    {
        get
        {
            if (!this.Any())
            {
                throw new Error("getting tail from empty stream");
            }
            return this with { Items = Items.Skip(1).ToImmutableList() };
        }
    }
}