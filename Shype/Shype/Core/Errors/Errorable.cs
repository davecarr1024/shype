namespace Shype.Core.Errors;

public abstract record Errorable<E> where E : Errorable<E>
{
    public class Error(Errorable<E> obj, IImmutableList<Error> Children, string message = "")
        : Errors.Error(Children.ToImmutableList<Errors.Error>(), message)
    {
        public Errorable<E> Object { get; init; } = obj;

        public override bool Equals(object? obj)
            => obj is Error error && ReferenceEquals(Object, error.Object) && base.Equals(error);

        public override int GetHashCode() => HashCode.Combine(Object, base.GetHashCode());

        protected override string ToStringLine() => $"{Object}: {base.ToStringLine()}";
    }

    protected Error CreateError(string message = "", params Error[] children)
        => new(this, children.ToImmutableList(), message);

    protected T Try<T>(Func<T> func, string message = "")
    {
        try
        {
            return func();
        }
        catch (Error error)
        {
            throw CreateError(message, error);
        }
    }
}