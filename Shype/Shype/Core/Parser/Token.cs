namespace Shype.Core.Parser;

public record Token(Lexer.Rule LexRule) : Head<Tokens.Token>
{
    public Token(string name, Regex.Regex value) : this(new Lexer.Rule(name, value)) { }

    public Token(string name, string value) : this(name, Regex.Regex.Create(value)) { }

    public Token(string name) : this(name, name) { }

    protected override Tokens.Token Apply(Tokens.Token head)
        => head.Type == LexRule.Name
            ? head
            : throw CreateError($"expected token to match {LexRule} but got {head}");

    public Parser<string> Value() => Transform(token => token.Value);

    public override Lexer.Lexer Lexer() => new(LexRule);

    public override string ToString() => LexRule.ToString();
}