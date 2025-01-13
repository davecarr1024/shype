namespace Shype.Core.Errors;

public class Error(string message) : Exception(message)
{
    public override bool Equals(object? obj) => obj is Error error && Message == error.Message;

    public override int GetHashCode() => base.GetHashCode();
}