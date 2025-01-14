using System.Collections;

namespace Shype.Core.Streams;

public abstract record Stream<S, T>(IImmutableList<T> Items)
    : Errors.Errorable<Stream<S, T>>, IEnumerable<T>
    where S : Stream<S, T>, new()
{
    public Stream(params T[] items) : this(items.ToImmutableList()) { }

    public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();

    public virtual bool Equals(Stream<S, T>? rhs) => rhs is not null && Items.SequenceEqual(rhs.Items);

    public override int GetHashCode() => Items.GetHashCode();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Items).GetEnumerator();

    protected void AssertNotEmpty()
    {
        if (!this.Any())
        {
            throw CreateError("empty stream");
        }
    }

    public T Head
    {
        get
        {
            AssertNotEmpty();
            return this.First();
        }
    }

    public S Tail
    {
        get
        {
            AssertNotEmpty();
            return new() { Items = this.Skip(1).ToImmutableList() };
        }
    }

    public static S operator +(Stream<S, T> lhs, Stream<S, T> rhs) => new() { Items = [.. lhs, .. rhs] };

    public static S operator +(Stream<S, T> lhs, T rhs) => new() { Items = [.. lhs, rhs] };

    public static S operator +(T lhs, Stream<S, T> rhs) => new() { Items = [lhs, .. rhs] };
}