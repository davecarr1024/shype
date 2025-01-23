namespace Shype.Core.Parser;

[TestClass]
public class OneOrMoreTest
{
    [TestMethod]
    public void TestApply()
    {
        var parser = Parser.Token("a").Value().OneOrMore().Transform(string.Concat);
        Assert.ThrowsException<Parser.Error>(() => parser.Apply(""));
        Assert.AreEqual(
            (new State(), "a"),
            parser.Apply("a")
        );
        Assert.AreEqual(
            (new State(), "aa"),
            parser.Apply("aa")
        );
    }

}