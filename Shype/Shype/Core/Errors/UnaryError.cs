namespace Shype.Core.Errors;

public class UnaryError : Error {
    public UnaryError(string message, Error child) : base(message) => Child = child;

    public Error Child { get; init; }
}