namespace Shype.Core.Errors;

public abstract record Errorable<E> where E : Errorable<E>
{
    public class Error(Errorable<E> Obj, string Message = "", params Errors.Error[] Children)
        : Errors.Error(CreateMessage(Obj, Message, Children))
    {
        protected static string CreateMessage(Errorable<E> obj, string message, IEnumerable<Errors.Error> children)
            => $"<{obj}> {CreateMessage(message, children)}";
    }

    protected Error CreateError(string message = "", params Errors.Error[] children)
        => new(this, message, children);

    protected T Try<T>(Func<T> func, string message = "")
    {
        try
        {
            return func();
        }
        catch (Errors.Error error)
        {
            throw CreateError(message, error);
        }
    }
}