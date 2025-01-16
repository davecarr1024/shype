
namespace Shype.Core.Streams;

public abstract record Stream<S, T>(IImmutableList<T> Items)
    : Errors.Errorable<Stream<S, T>>, IEnumerable<T>
    where S : Stream<S, T>
{
    public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Items).GetEnumerator();

    public virtual bool Equals(Stream<S, T>? rhs)
        => rhs is not null && Items.SequenceEqual(rhs.Items);

    public override int GetHashCode() => Items.GetHashCode();

    public override string ToString()
        => $"[{string.Join(", ", this.Select(c => c?.ToString()))}]";

    public bool Empty() => !this.Any();

    protected void AssertNotEmpty()
    {
        if (Empty())
        {
            throw CreateError("empty stream");
        }
    }

    public T Head()
    {
        AssertNotEmpty();
        return this.First();
    }

    public S Tail()
    {
        AssertNotEmpty();
        return (S)this with { Items = this.Skip(1).ToImmutableList() };
    }

    public static S operator +(Stream<S, T> lhs, Stream<S, T> rhs) => (S)lhs with { Items = [.. lhs, .. rhs] };

    public static S operator +(Stream<S, T> lhs, T rhs) => (S)lhs with { Items = [.. lhs, rhs] };

    public static S operator +(T lhs, Stream<S, T> rhs) => (S)rhs with { Items = [lhs, .. rhs] };
}