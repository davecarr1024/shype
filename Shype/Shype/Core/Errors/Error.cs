using System.Collections;

namespace Shype.Core.Errors;

public class Error(string message = "", params Error[] Children)
    : Exception(CreateMessage(message, Children))
{
    protected static string CreateMessage(string message, IEnumerable<Error> children)
        => (message, children.Count()) switch
        {
            ("", 0) => "empty error",
            (_, 0) => message,
            ("", _) => PrefixChildren(children, ""),
            (_, _) => $"{message}\n{PrefixChildren(children, "  ")}"
        };

    private static string PrefixChildren(IEnumerable<Error> children, string prefix)
        => string.Join('\n', from child in children select PrefixLines(child.Message, prefix));

    private static string PrefixLines(string message, string prefix)
        => string.Join('\n', from line in message.Split('\n') select $"{prefix}{line}");

    public override bool Equals(object? obj)
        => obj is Error error && Message == error.Message;

    public override int GetHashCode() => base.GetHashCode();
}