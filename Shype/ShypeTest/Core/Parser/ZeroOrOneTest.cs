namespace Shype.Core.Parser;

[TestClass]
public class ZeroOrOneTest
{
    [TestMethod]
    public void TestApply()
    {
        var parser = Parser.Token("a").Value().ZeroOrOne();
        Assert.AreEqual(
            (new State(), null),
            parser.Apply("")
        );
        Assert.AreEqual(
            (new State(), "a"),
            parser.Apply("a")
        );
        Assert.AreEqual(
            (new State(new Tokens.Token("a", "a", new(0, 1))), "a"),
            parser.Apply("aa")
        );
    }

}