namespace Shype.Core.Regex;

[TestClass]
public class OneOrMoreTest
{
    [TestMethod]
    public void TestApply()
    {
        OneOrMore regex = Regex.Literal('a').OneOrMore();
        Assert.ThrowsException<Regex.Error>(() => regex.Apply(new()));
        Assert.ThrowsException<Regex.Error>(() => regex.Apply("b"));
        Assert.AreEqual(
            (new State("b", new(0, 1)), new Result("a")),
            Regex.Literal('a').OneOrMore().Apply(new("ab"))
        );
        Assert.AreEqual(
            (new State("b", new(0, 2)), new Result("aa")),
            Regex.Literal('a').OneOrMore().Apply(new("aab"))
        );
        Assert.AreEqual(
            (new State(), new Result("a")),
            Regex.Literal('a').OneOrMore().Apply(new("a"))
        );
        Assert.AreEqual(
            (new State(), new Result("aa")),
            Regex.Literal('a').OneOrMore().Apply(new("aa"))
        );
    }

    [TestMethod]
    public void TestToString()
    {
        Assert.AreEqual(
            "a+",
            Regex.Literal('a').OneOrMore().ToString()
        );
    }
}