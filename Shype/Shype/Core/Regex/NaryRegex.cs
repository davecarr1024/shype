namespace Shype.Core.Regex;

public abstract record NaryRegex(IImmutableList<Regex> Children) : Regex, IEnumerable<Regex>
{
    public IEnumerator<Regex> GetEnumerator() => Children.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Children).GetEnumerator();

    public virtual bool Equals(NaryRegex? rhs) => rhs is not null && Children.SequenceEqual(rhs.Children);

    public override int GetHashCode() => Children.GetHashCode();

    protected (State state, Result result) ApplyChild(Regex child, State state)
    {
        if (!Children.Contains(child))
        {
            throw CreateError($"unknown child {child}");
        }
        return Try(() => child.Apply(state), $"error while applying to child {child}");
    }

    public override string ToString() => base.ToString();

    protected string ToString(bool first, string separator)
        => first switch
        {
            true => string.Join(separator, this.Select(child => child.ToString(false))),
            false => $"({ToString(true, separator)})",
        };
}
