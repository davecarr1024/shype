namespace Shype.Core.Tokens;

public record Stream(IImmutableList<Token> Items)
    : Streams.Stream<Stream, Token>(Items)
{
    public Stream(params Token[] tokens) : this(tokens.ToImmutableList()) { }

    public override string ToString() => base.ToString();
}