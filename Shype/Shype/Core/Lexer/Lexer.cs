
namespace Shype.Core.Lexer;


public record Lexer(IImmutableList<Rule> Rules)
    : Errors.Errorable<Lexer>, IEnumerable<Rule>
{
    public Lexer(params Rule[] rules) : this(rules.ToImmutableList()) { }

    public IEnumerator<Rule> GetEnumerator() => Rules.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Rules).GetEnumerator();

    public virtual bool Equals(Lexer? rhs) => rhs is not null && Rules.SequenceEqual(rhs.Rules);

    public override int GetHashCode() => Rules.GetHashCode();

    protected (Regex.State, Tokens.Token, bool include) ApplyAnyRule(Regex.State state)
    {
        List<Errors.Error> errors = [];
        foreach (Rule rule in this)
        {
            try
            {
                (state, Tokens.Token token) = rule.Apply(state);
                return (state, token, rule.Include);
            }
            catch (Errors.Error error)
            {
                errors.Add(error);
            }
        }
        throw CreateError($"failed to apply any lexer rule to {state}", [.. errors]);
    }

    protected Result Apply(Regex.State state)
    {
        Result result = new();
        while (state.Any())
        {
            (state, Tokens.Token token, bool include) = ApplyAnyRule(state);
            if (include)
            {
                result += token;
            }
        }
        return result;
    }

    public Result Apply(string input, Chars.Position? position = null)
        => Apply(new(input, position));

    public static Lexer operator +(Lexer lhs, Lexer rhs)
        => lhs with { Rules = [.. lhs, .. rhs] };

    public static Lexer operator +(Lexer lhs, Rule rhs)
        => lhs with { Rules = [.. lhs, rhs] };

    public static Lexer operator +(Rule lhs, Lexer rhs)
        => rhs with { Rules = [lhs, .. rhs] };
}