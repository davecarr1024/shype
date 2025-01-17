namespace Shype.Core.Regex;

[TestClass]
public class ZeroOrMoreTest
{
    [TestMethod]
    public void TestApply()
    {
        Regex regex = Regex.Literal('a').ZeroOrMore();
        Assert.AreEqual(
            (new State(), new Result()),
            regex.Apply("")
        );
        Assert.AreEqual(
            (new State("b"), new Result()),
            Regex.Literal('a').ZeroOrMore().Apply("b")
        );
        Assert.AreEqual(
            (new State("b", new(0, 1)), new Result("a")),
            Regex.Literal('a').ZeroOrMore().Apply("ab")
        );
        Assert.AreEqual(
            (new State("b", new(0, 2)), new Result("aa")),
            Regex.Literal('a').ZeroOrMore().Apply("aab")
        );
        Assert.AreEqual(
            (new State(), new Result("a")),
            Regex.Literal('a').ZeroOrMore().Apply("a")
        );
        Assert.AreEqual(
            (new State(), new Result("aa")),
            Regex.Literal('a').ZeroOrMore().Apply("aa")
        );
    }

    [TestMethod]
    public void TestToString()
    {
        Assert.AreEqual(
            "a*",
            Regex.Literal('a').ZeroOrMore().ToString()
        );
    }
}

