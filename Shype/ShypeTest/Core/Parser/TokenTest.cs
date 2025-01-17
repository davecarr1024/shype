namespace Shype.Core.Parser;

[TestClass]
public class TokenTest
{
    [TestMethod]
    public void TestApply()
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
    public void TestApplyValue()
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

}