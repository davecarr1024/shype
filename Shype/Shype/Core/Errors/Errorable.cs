namespace Shype.Core.Errors;

public abstract record Errorable<E> where E : Errorable<E>
{
    protected class Error(Errorable<E> obj, string message = "") : Errors.Error(message)
    {
        public Errorable<E> Object { get; init; } = obj;

        public override bool Equals(object? obj) => obj is Error error && ReferenceEquals(Object, error.Object) && base.Equals(error);

        public override int GetHashCode() => HashCode.Combine(Object, base.GetHashCode());
    }

    protected class UnaryError(Errorable<E> obj, Error child, string message = "") : Error(obj, message)
    {
        public Error Child { get; init; } = child;

        public override bool Equals(object? obj) => obj is UnaryError error && Child == error.Child && base.Equals(error);

        public override int GetHashCode() => HashCode.Combine(Child, base.GetHashCode());
    }

    protected class NaryError(Errorable<E> obj, List<Error> children, string message = "") : Error(obj, message)
    {
        public List<Error> Children { get; init; } = children;

        public override bool Equals(object? obj) => obj is NaryError error && Children.SequenceEqual(error.Children) && base.Equals(error);

        public override int GetHashCode() => HashCode.Combine(Children, base.GetHashCode());
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