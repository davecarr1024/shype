namespace Shype.Core.Lexer;

[TestClass]
public class RuleTest
{
    [TestMethod]
    public void TestApply()
    {
        Assert.ThrowsException<Rule.Error>(() => new Rule("r", "a").Apply("b"));
        Assert.AreEqual(
            (new Regex.State(), new Tokens.Token("r", "a", new(0, 0))),
            new Rule("r", "a").Apply("a")
        );
        Assert.AreEqual(
            (new Regex.State("b", new(0, 1)), new Tokens.Token("r", "a", new(0, 0))),
            new Rule("r", "a").Apply("ab")
        );
        Assert.AreEqual(
            (new Regex.State(), new Tokens.Token("r", "a", new(1, 2))),
            new Rule("r", "a").Apply("a", new(1, 2))
        );
    }
}