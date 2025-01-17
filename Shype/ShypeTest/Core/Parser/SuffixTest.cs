namespace Shype.Core.Parser;

[TestClass]
public class SuffixTest
{
    [TestMethod]
    public void TestApply()
    {
        Suffix<string> parser = Parser.Token("a").Value() & "b";
        Assert.ThrowsException<Parser.Error>(() => parser.Apply(""));
        Assert.ThrowsException<Parser.Error>(() => parser.Apply("a"));
        Assert.ThrowsException<Parser.Error>(() => parser.Apply("b"));
        Assert.AreEqual(
            (new State(), "a"),
            parser.Apply("ab")
        );
        Assert.AreEqual(
            (new State(new Tokens.Token("b", "b", new(0, 2))), "a"),
            parser.Apply("abb")
        );
    }
}