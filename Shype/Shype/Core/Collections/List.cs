using System.Collections;
using Shype.Core.Errors;

namespace Shype.Core.Collections;

public record List<T>(IImmutableList<T> Items) : Errorable<List<T>>, IImmutableList<T>
{
    public List(params T[] items) : this(items.ToImmutableList()) { }

    public T this[int index] => Items[index];

    public IImmutableList<T> Items { get; init; } = Items;

    public virtual bool Equals(List<T>? other) => other is List<T> list && Items.SequenceEqual(list.Items);

    public override int GetHashCode() => Items.GetHashCode();

    public int Count => Items.Count;

    public IImmutableList<T> Add(T value) => Items.Add(value);

    public IImmutableList<T> AddRange(IEnumerable<T> items) => Items.AddRange(items);

    public IImmutableList<T> Clear() => Items.Clear();

    public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();

    public int IndexOf(T item, int index, int count, IEqualityComparer<T>? equalityComparer) => Items.IndexOf(item, index, count, equalityComparer);

    public IImmutableList<T> Insert(int index, T element) => Items.Insert(index, element);

    public IImmutableList<T> InsertRange(int index, IEnumerable<T> items) => Items.InsertRange(index, items);

    public int LastIndexOf(T item, int index, int count, IEqualityComparer<T>? equalityComparer) => Items.LastIndexOf(item, index, count, equalityComparer);

    public IImmutableList<T> Remove(T value, IEqualityComparer<T>? equalityComparer) => Items.Remove(value, equalityComparer);

    public IImmutableList<T> RemoveAll(Predicate<T> match) => Items.RemoveAll(match);

    public IImmutableList<T> RemoveAt(int index) => Items.RemoveAt(index);

    public IImmutableList<T> RemoveRange(IEnumerable<T> items, IEqualityComparer<T>? equalityComparer) => Items.RemoveRange(items, equalityComparer);

    public IImmutableList<T> RemoveRange(int index, int count) => Items.RemoveRange(index, count);

    public IImmutableList<T> Replace(T oldValue, T newValue, IEqualityComparer<T>? equalityComparer) => Items.Replace(oldValue, newValue, equalityComparer);

    public IImmutableList<T> SetItem(int index, T value) => Items.SetItem(index, value);

    IEnumerator IEnumerable.GetEnumerator() => Items.GetEnumerator();
}