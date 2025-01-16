using System.Collections;

namespace Shype.Core.Errors;

public class Error(string message = "", params Error[] Children)
    : Exception(CreateMessage(message, Children))
{
    protected static string CreateMessage(string message, IEnumerable<Error> children)
        => children.Any()
            ? $"{message}: " + string.Join(", ", children.Select(child => child.Message))
            : message;

    public override bool Equals(object? obj)
        => obj is Error error && Message == error.Message;

    public override int GetHashCode() => base.GetHashCode();
}