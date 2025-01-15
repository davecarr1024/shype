namespace Shype.Core.Chars;

public record Position(int Line, int Column)
{
    public Position() : this(0, 0) { }

    public override string ToString() => $"({Line},{Column})";
}