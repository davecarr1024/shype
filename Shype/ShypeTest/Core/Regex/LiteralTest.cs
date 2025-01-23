namespace Shype.Core.Regex;

[TestClass]
public class LiteralTest
{
    [TestMethod]
    public void TestApply()
    {
        Assert.ThrowsException<Regex.Error>(() => Regex.Literal('a').Apply("b"));
        Assert.AreEqual(
            (new State(), new Result("a")),
            Regex.Literal('a').Apply("a")
        );
    }

    [TestMethod]
    public void TestToString()
    {
        Assert.AreEqual("a", Regex.Literal('a').ToString());
    }

    [TestMethod]
    public void TestParse()
    {
        Assert.ThrowsException<Parser.Parser.Error>(() => Literal.Parser().Apply(""));
        Assert.ThrowsException<Parser.Parser.Error>(() => Literal.Parser().Apply("("));
        Assert.AreEqual(
            (new Parser.State(), Regex.Literal('a')),
            Literal.Parser().Apply("a")
        );
        Assert.AreEqual(
            (
                new Parser.State([
                    new Tokens.Token("literal", "b", new(0, 1))
                ]),
                Regex.Literal('a')
            ),
            Literal.Parser().Apply("ab")
        );
    }
}

