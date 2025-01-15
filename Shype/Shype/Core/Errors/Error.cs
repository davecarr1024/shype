using System.Collections;

namespace Shype.Core.Errors;

public class Error(IImmutableList<Error> Children, string message)
    : Exception(message), IEnumerable<Error>
{
    public IImmutableList<Error> Children { get; init; } = Children;

    public override bool Equals(object? obj)
        => obj is Error error && Message == error.Message && Children.SequenceEqual(error.Children);

    public IEnumerator<Error> GetEnumerator() => Children.GetEnumerator();

    public override int GetHashCode() => base.GetHashCode();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Children).GetEnumerator();

    protected virtual string ToStringLine() => Message;

    protected string ToString(int tabs)
        => string.Concat(Enumerable.Repeat("  ", tabs))
            + ToStringLine()
            + string.Concat(Children.Select(child => $"\n{child.ToString(tabs + 1)}"));

    public override string ToString() => ToString(0);
}