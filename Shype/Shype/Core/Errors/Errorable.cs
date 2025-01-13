namespace Shype.Core.Errors;

public abstract record Errorable<E> where E : Errorable<E>
{
    public class Error(Errorable<E> obj, string message = "") : Errors.Error(message)
    {
        public Errorable<E> Object { get; init; } = obj;
    }

    public class UnaryError(Errorable<E> obj, Error child, string message = "") : Error(obj, message)
    {
        public Error Child { get; init; } = child;
    }

    public class NaryError(Errorable<E> obj, List<Error> children, string message = "") : Error(obj, message)
    {
        public List<Error> Children { get; init; } = children;
    }

    protected Error CreateError(string message = "") => new(this, message);

    protected UnaryError CreateError(Error child, string message = "") => new(this, child, message);

    protected NaryError CreateError(List<Error> children, string message = "") => new(this, children, message);

    protected T Try<T>(Func<T> func, string message = "")
    {
        try
        {
            return func();
        }
        catch (Error child)
        {
            throw CreateError(child, message);
        }
    }
}