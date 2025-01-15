namespace Shype.Core.Chars;

public record Char(char Value, Position Position)
{
    public Char(char value) : this(value, new(0, 0)) { }

    public override string ToString() => $"{Value}@{Position}";
}
