namespace Shype.Core.Errors;

public class NaryError : Error {
    private NaryError(string message, List<Error> children) : base(message) => Children = children;

    public NaryError(string message, params Error[] children) : this(message, new List<Error>(children)) {}

    public List<Error> Children { get; init; }
}