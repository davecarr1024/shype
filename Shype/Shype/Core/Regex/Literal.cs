namespace Shype.Core.Regex;

public record Literal(char Value) : HeadRegex
{
    internal override string ToString(bool first) => Value.ToString();

    public override Chars.Char Apply(Chars.Char head)
    {
        if (head.Value != Value)
        {
            throw CreateError($"expected literal {Value}");
        }
        return head;
    }

    public static Parser.Parser<Literal> Parser()
        => Core.Parser.Parser
            .Token(new Lexer.Rule("literal", Operators().Not()))
            .Value()
            .Transform(value => Literal(value.Single()));
}
