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
        Token a = Parser.Token("a"),
            b = Parser.Token("b"),
            c = Parser.Token("c"),
            d = Parser.Token("d");
        Assert.AreEqual(
            new And<Tokens.Token>([
                a,
                b,
                c
            ]),
            a & b & c
        );
        Assert.AreEqual(
            new And<Tokens.Token>([
                a,
                b,
                c
            ]),
            a & (b & c)
        );
        Assert.AreEqual(
            new And<Tokens.Token>([
                a,
                b,
                c,
                d
            ]),
            a & b & (c & d)
        );
    }
}