namespace Shype.Core.Parser;

[TestClass]
public class ParserTest
{
    [TestMethod]
    public void TestToken()
    {
        Token parser = Parser.Token("r", "a");
        Assert.ThrowsException<Parser.Error>(() => parser.Apply(new()));
        Assert.ThrowsException<Parser.Error>(() => parser.Apply("b"));
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
        Parser<string> parser = Parser.Token("r", "a").Value();
        Assert.ThrowsException<Parser.Error>(() => parser.Apply(new()));
        Assert.ThrowsException<Parser.Error>(() => parser.Apply("b"));
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
        var parser = Parser.Token("a").Value().ZeroOrMore().Transform(string.Concat);
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

    [TestMethod]
    public void TestAnd()
    {
        Parser<string> parser =
            (Parser.Token("a").Value() & Parser.Token("b").Value())
            .Transform(string.Concat);
        Assert.ThrowsException<Parser.Error>(() => parser.Apply(""));
        Assert.ThrowsException<Parser.Error>(() => parser.Apply("a"));
        Assert.ThrowsException<Parser.Error>(() => parser.Apply("b"));
        Assert.ThrowsException<Parser.Error>(() => parser.Apply("ac"));
        Assert.AreEqual(
            (new State(), "ab"),
            parser.Apply("ab")
        );
        Assert.AreEqual(
            (new State(new Tokens.Token("a", "a", new(0, 2))), "ab"),
            parser.Apply("aba")
        );
    }

    [TestMethod]
    public void TestAndCombine()
    {
        Assert.AreEqual(
            new And<Tokens.Token>([
                Parser.Token("a"),
                Parser.Token("b"),
                Parser.Token("c")
            ]),
            Parser.Token("a") & Parser.Token("b") & Parser.Token("c")
        );
        Assert.AreEqual(
            new And<Tokens.Token>([
                Parser.Token("a"),
                Parser.Token("b"),
                Parser.Token("c")
            ]),
            Parser.Token("a") & (Parser.Token("b") & Parser.Token("c"))
        );
    }
}