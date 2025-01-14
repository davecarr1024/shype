namespace Shype.Core.Tokens;

public record Token(string Type, string Value, Chars.Position Position) : Errors.Errorable<Token>;