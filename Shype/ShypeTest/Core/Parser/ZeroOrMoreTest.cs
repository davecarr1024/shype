namespace Shype.Core.Parser;

[TestClass]
public class ZeroOrMoreTest
{
    [TestMethod]
    public void TestApply()
    {
        var parser = Parser.Token("a").Value().ZeroOrMore().Transform(string.Concat);
        Assert.AreEqual(
            (new State(), ""),
            parser.Apply("")
        );
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