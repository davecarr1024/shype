namespace Shype.Core.Chars;

public record Char(char Value, Position Position)
{
    public override string ToString() => $"{Value}@{Position}";
}
