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
                throw CreateError("getting head from empty stream");
            }
            return this[0];
        }
    }

    public Stream<S, T> Tail
    {
        get
        {
            if (!this.Any())
            {
                throw CreateError("getting tail from empty stream");
            }
            return this with { Items = Items.Skip(1).ToImmutableList() };
        }
    }
}