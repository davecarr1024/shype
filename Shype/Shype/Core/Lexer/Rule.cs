
namespace Shype.Core.Lexer;

public record Rule(string Name, Regex.Regex Regex, bool Include = true)
        : Errors.Errorable<Rule>
{
    public Rule(string name, string regex, bool include = true)
        : this(name, Core.Regex.Regex.Create(regex), include) { }

    public (Regex.State, Tokens.Token) Apply(Regex.State state)
    {
        (state, Regex.Result result) = Try(() => Regex.Apply(state), $"failed to apply lexer rule {Name}");
        return (state, new(Name, result.Value(), result.Position()));
    }

    public (Regex.State, Tokens.Token) Apply(string input, Chars.Position? position = null)
        => Apply(new(input, position));

    public override string ToString() =>
        (Name, Regex, Include) switch
        {
            (_, _, true) when Name == Regex.ToString() => Name,
            (_, _, false) when Name == Regex.ToString() => $"~{Name}",
            (_, _, false) => $"~{Name}({Regex})",
            (_, _, true) => $"{Name}({Regex})",
        };
}
