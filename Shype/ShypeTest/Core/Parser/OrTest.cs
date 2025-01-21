namespace Shype.Core.Parser;

[TestClass]
public class OrTest
{
    [TestMethod]
    public void TestApply()
    {
        Parser<string> parser =
            (
                Parser.Token("a").Value()
                | Parser.Token("b").Value()
            ) & "c";
        Assert.ThrowsException<Parser.Error>(() => parser.Apply(""));
        Assert.ThrowsException<Parser.Error>(() => parser.Apply("c"));
        Assert.AreEqual(
            (new State(), "a"),
            parser.Apply("ac")
        );
        Assert.AreEqual(
            (new State(), "b"),
            parser.Apply("bc")
        );
        Assert.AreEqual(
            (new State(new Tokens.Token("b", "b", new(0, 2))), "a"),
            parser.Apply("acb")
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
            new Or<Tokens.Token>([
                a,
                b,
                c
            ]),
            a | b | c
        );
        Assert.AreEqual(
            new Or<Tokens.Token>([
                a,
                b,
                c
            ]),
            a | (b | c)
        );
        Assert.AreEqual(
            new Or<Tokens.Token>([
                a,
                b,
                c,
                d
            ]),
            a | b | (c | d)
        );
    }
}