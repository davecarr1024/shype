namespace Shype.Core.Parser;

[TestClass]
public class PrefixTest
{
    [TestMethod]
    public void TestApply()
    {
        Prefix<string> parser = "b" & Parser.Token("a").Value();
        // Assert.ThrowsException<Parser.Error>(() => parser.Apply(""));
        // Assert.ThrowsException<Parser.Error>(() => parser.Apply("a"));
        // Assert.ThrowsException<Parser.Error>(() => parser.Apply("b"));
        Assert.AreEqual(
            (new State(), "a"),
            parser.Apply("ba")
        );
        Assert.AreEqual(
            (new State(new Tokens.Token("b", "b", new(0, 2))), "a"),
            parser.Apply("bab")
        );
    }
}