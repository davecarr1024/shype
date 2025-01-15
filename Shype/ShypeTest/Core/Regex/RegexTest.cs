namespace Shype.Core.Regex;

[TestClass]
public class RegexTest
{
    [TestMethod]
    public void TestLiteral()
    {
        Assert.ThrowsException<Regex.Error>(() => Regex.Literal('a').Apply(new("b")));
        Assert.AreEqual(
            (new State(), new Result("a")),
            Regex.Literal('a').Apply(new("a"))
        );
    }

    [TestMethod]
    public void TestZeroOrMore()
    {
        Regex regex = Regex.Literal('a').ZeroOrMore();
        Assert.AreEqual(
            (new State(), new Result()),
            regex.Apply(new())
        );
        Assert.AreEqual(
            (new State("b"), new Result()),
            Regex.Literal('a').ZeroOrMore().Apply(new("b"))
        );
        Assert.AreEqual(
            (new State("b", new(0, 1)), new Result("a")),
            Regex.Literal('a').ZeroOrMore().Apply(new("ab"))
        );
        Assert.AreEqual(
            (new State("b", new(0, 2)), new Result("aa")),
            Regex.Literal('a').ZeroOrMore().Apply(new("aab"))
        );
        Assert.AreEqual(
            (new State(), new Result("a")),
            Regex.Literal('a').ZeroOrMore().Apply(new("a"))
        );
        Assert.AreEqual(
            (new State(), new Result("aa")),
            Regex.Literal('a').ZeroOrMore().Apply(new("aa"))
        );
    }

    [TestMethod]
    public void TestOneOrMore()
    {
        Regex regex = Regex.Literal('a').OneOrMore();
        Assert.ThrowsException<Regex.Error>(() => regex.Apply(new()));
        Assert.ThrowsException<Regex.Error>(() => regex.Apply(new("b")));
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
    public void TestZeroOrOne()
    {
        Regex regex = Regex.Literal('a').ZeroOrOne();
        Assert.AreEqual(
            (new State(), new Result()),
            regex.Apply(new())
        );
        Assert.AreEqual(
            (new State("b"), new Result()),
            Regex.Literal('a').ZeroOrMore().Apply(new("b"))
        );
        Assert.AreEqual(
            (new State("b", new(0, 1)), new Result("a")),
            Regex.Literal('a').ZeroOrOne().Apply(new("ab"))
        );
        Assert.AreEqual(
            (new State(), new Result("a")),
            Regex.Literal('a').ZeroOrOne().Apply(new("a"))
        );
    }

    [TestMethod]
    public void TestAnd()
    {
        Regex regex = Regex.Literal('a') & Regex.Literal('b');
        Assert.AreEqual(
            (new State(), new Result("ab")),
            regex.Apply("ab")
        );
        Assert.AreEqual(
            (new State("c", new(0, 2)), new Result("ab")),
            regex.Apply(new("abc"))
        );
        Assert.ThrowsException<Regex.Error>(
            () => regex.Apply("a")
        );
        Assert.ThrowsException<Regex.Error>(
            () => regex.Apply(new("c"))
        );
        Assert.ThrowsException<Regex.Error>(
            () => regex.Apply(new("b"))
        );
        Assert.ThrowsException<Regex.Error>(
            () => regex.Apply(new("c"))
        );
    }

    [TestMethod]
    public void TestAny()
    {
        Regex regex = Regex.Any();
        Assert.ThrowsException<Regex.Error>(() => regex.Apply(new()));
        Assert.AreEqual(
            (new State(), new Result("a")),
            regex.Apply("a")
        );
        Assert.AreEqual(
            (new State("b", new(0, 1)), new Result("a")),
            regex.Apply("ab")
        );
    }

    [TestMethod]
    public void TestRange()
    {
        Regex regex = Regex.Range('a', 'z');
        Assert.ThrowsException<Regex.Error>(() => regex.Apply(""));
        Assert.ThrowsException<Regex.Error>(() => regex.Apply("A"));
        Assert.AreEqual(
            (new State(), new Result("a")),
            regex.Apply("a")
        );
        Assert.AreEqual(
            (new State(), new Result("b")),
            regex.Apply("b")
        );
        Assert.AreEqual(
            (new State(), new Result("z")),
            regex.Apply("z")
        );
        Assert.AreEqual(
            (new State("b", new(0, 1)), new Result("a")),
            regex.Apply("ab")
        );
    }

    [TestMethod]
    public void TestClass()
    {
        Regex regex = Regex.Digits();
        Assert.ThrowsException<Regex.Error>(() => regex.Apply(""));
        Assert.ThrowsException<Regex.Error>(() => regex.Apply("a"));
        Assert.AreEqual(
            (new State(), new Result("0")),
            regex.Apply("0")
        );
        Assert.AreEqual(
            (new State("b", new(0, 1)), new Result("0")),
            regex.Apply("0b")
        );
        Assert.AreEqual("\\d", regex.ToString());
    }

    [TestMethod]
    public void TestNot()
    {
        Assert.ThrowsException<NotImplementedException>((Regex.Literal('a') & Regex.Literal('b')).Not);
        Regex regex = Regex.Literal('a').Not();
        Assert.ThrowsException<Regex.Error>(() => regex.Apply(""));
        Assert.ThrowsException<Regex.Error>(() => regex.Apply("a"));
        Assert.AreEqual(
            (new State(), new Result("b")),
            regex.Apply("b")
        );
    }
}
