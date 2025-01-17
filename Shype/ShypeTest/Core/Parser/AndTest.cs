namespace Shype.Core.Parser;

[TestClass]
public class AndTest
{
    [TestMethod]
    public void TestApply()
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
    public void TestCombine()
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