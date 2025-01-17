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
        Assert.AreEqual(
            new Or<Tokens.Token>([
                Parser.Token("a"),
                Parser.Token("b"),
                Parser.Token("c")
            ]),
            Parser.Token("a") | Parser.Token("b") | Parser.Token("c")
        );
        Assert.AreEqual(
            new Or<Tokens.Token>([
                Parser.Token("a"),
                Parser.Token("b"),
                Parser.Token("c")
            ]),
            Parser.Token("a") | (Parser.Token("b") | Parser.Token("c"))
        );
    }
}