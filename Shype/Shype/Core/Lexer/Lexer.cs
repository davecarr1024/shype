
namespace Shype.Core.Lexer;

public record Lexer(IImmutableList<Lexer.Rule> Rules)
    : Errors.Errorable<Lexer>, IEnumerable<Lexer.Rule>
{
    public Lexer(params Rule[] rules) : this(rules.ToImmutableList()) { }

    public IEnumerator<Rule> GetEnumerator() => Rules.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Rules).GetEnumerator();

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
    }

    protected (Regex.State, Tokens.Token) ApplyAnyRule(Regex.State state)
    {
        foreach (Rule rule in this)
        {
            return rule.Apply(state);
        }
        throw CreateError($"failed to apply any lexer rule to {state}");
    }
}