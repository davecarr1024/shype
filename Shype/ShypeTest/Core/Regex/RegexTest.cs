namespace Shype.Core.Regex;

[TestClass]
public class RegexTest
{
    [TestMethod]
    public void TestLiteral()
    {
        Assert.ThrowsException<Regex.Error>(() => Regex.Literal('a').Apply(new State("b")));
        Assert.AreEqual(
            (new State(), new Result("a")),
            Regex.Literal('a').Apply(new State("a"))
        );
    }

    [TestMethod]
    public void TestZeroOrMore()
    {
        Regex regex = Regex.Literal('a').ZeroOrMore();
        Assert.AreEqual(
            (new State(), new Result()),
            regex.Apply(new State())
        );
        Assert.AreEqual(
            (new State("b"), new Result()),
            Regex.Literal('a').ZeroOrMore().Apply(new State("b"))
        );
        Assert.AreEqual(
            (new State("b", new(0, 1)), new Result("a")),
            Regex.Literal('a').ZeroOrMore().Apply(new State("ab"))
        );
        Assert.AreEqual(
            (new State("b", new(0, 2)), new Result("aa")),
            Regex.Literal('a').ZeroOrMore().Apply(new State("aab"))
        );
        Assert.AreEqual(
            (new State(), new Result("a")),
            Regex.Literal('a').ZeroOrMore().Apply(new State("a"))
        );
        Assert.AreEqual(
            (new State(), new Result("aa")),
            Regex.Literal('a').ZeroOrMore().Apply(new State("aa"))
        );
    }

    [TestMethod]
    public void TestAnd()
    {
        Regex regex = Regex.Literal('a') & Regex.Literal('b');
        Assert.AreEqual(
            (new State(), new Result("ab")),
            regex.Apply(new State("ab"))
        );
        Assert.AreEqual(
            (new State("c", new(0, 2)), new Result("ab")),
            regex.Apply(new State("abc"))
        );
        Assert.ThrowsException<Regex.Error>(
            () => regex.Apply(new State("a"))
        );
        Assert.ThrowsException<Regex.Error>(
            () => regex.Apply(new State("c"))
        );
        Assert.ThrowsException<Regex.Error>(
            () => regex.Apply(new State("b"))
        );
        Assert.ThrowsException<Regex.Error>(
            () => regex.Apply(new State("c"))
        );
    }
}