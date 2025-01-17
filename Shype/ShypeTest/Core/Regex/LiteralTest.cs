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
}

