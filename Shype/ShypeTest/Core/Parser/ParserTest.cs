namespace Shype.Core.Parser;

[TestClass]
public class ParserTest
{
    [TestMethod]
    public void TestToken()
    {
        Token parser = new("r", "a");
        Assert.ThrowsException<Token.Error>(() => parser.Apply(new()));
        Assert.ThrowsException<Token.Error>(() => parser.Apply("b"));
        Assert.AreEqual(
            (new State(), new Tokens.Token("r", "a")),
            parser.Apply(new(new Tokens.Token("r", "a")))
        );
        Assert.AreEqual(
            (new State(), new Tokens.Token("r", "a")),
            parser.Apply("a")
        );
        Assert.AreEqual(
            (new State(new Tokens.Token("r", "a", new(0, 1))), new Tokens.Token("r", "a")),
            parser.Apply("aa")
        );
    }

    [TestMethod]
    public void TestTokenValue()
    {
        Parser<string> parser = new Token("r", "a").Value();
        Assert.ThrowsException<Parser<string>.Error>(() => parser.Apply(new()));
        Assert.ThrowsException<Parser<string>.Error>(() => parser.Apply("b"));
        Assert.AreEqual(
            (new State(), "a"),
            parser.Apply(new(new Tokens.Token("r", "a")))
        );
        Assert.AreEqual(
            (new State(), "a"),
            parser.Apply("a")
        );
        Assert.AreEqual(
            (new State(new Tokens.Token("r", "a", new(0, 1))), "a"),
            parser.Apply("aa")
        );
    }

    [TestMethod]
    public void TestZeroOrMore()
    {
        var parser = new Token("a").Value().ZeroOrMore().Transform(string.Concat);
        Assert.AreEqual(
            (new State(), ""),
            parser.Apply("")
        );
        Assert.AreEqual(
            (new State(), "a"),
            parser.Apply("a")
        );
        Assert.AreEqual(
            (new State(), "aa"),
            parser.Apply("aa")
        );
    }
}