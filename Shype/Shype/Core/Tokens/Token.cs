namespace Shype.Core.Tokens;

public record Token(string Type, string Value, Chars.Position Position)
    : Errors.Errorable<Token>
{
    public Token(string type, string value) : this(type, value, new(0, 0)) { }
}