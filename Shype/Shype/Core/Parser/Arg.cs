namespace Shype.Core.Parser;

public abstract record Arg<O>
{
    public abstract O Apply(O obj);
}

public partial record Arg<O, T>(Func<O, T, O> Setter, T Value) : Arg<O>
{
    public override O Apply(O obj) => Setter(obj, Value);
}

