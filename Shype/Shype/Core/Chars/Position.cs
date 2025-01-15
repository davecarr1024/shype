namespace Shype.Core.Chars;

public record Position(int Line, int Column)
{
    public override string ToString() => $"({Line},{Column})";
}