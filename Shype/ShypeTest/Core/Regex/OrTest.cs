namespace Shype.Core.Regex;

[TestClass]
public class OrTest
{
    [TestMethod]
    public void TestApply()
    {
        Regex regex = Regex.Literal('a') | Regex.Literal('b');
        Assert.ThrowsException<Regex.Error>(() => regex.Apply(""));
        Assert.ThrowsException<Regex.Error>(() => regex.Apply("c"));
        Assert.AreEqual(
            (new State(), new Result("a")),
            regex.Apply("a")
        );
        Assert.AreEqual(
            (new State(), new Result("b")),
            regex.Apply("b")
        );
        Assert.AreEqual(
            (new State("c", new(0, 1)), new Result("b")),
            regex.Apply("bc")
        );
    }

    [TestMethod]
    public void TestCombine()
    {
        Literal a = new('a'), b = new('b'), c = new('c'), d = new('d');
        Assert.AreEqual(a, Regex.Or(a));
        Assert.AreEqual(
            Regex.Or(a, b, c),
            a | b | c
        );
        Assert.AreEqual(
            Regex.Or(a, b, c),
            a | (b | c)
        );
        Assert.AreEqual(
            Regex.Or(a, b, c, d),
            a | b | (c | d)
        );
    }

    [TestMethod]
    public void TestToString()
    {
        Literal a = new('a'), b = new('b'), c = new('c');
        Assert.AreEqual(
            "a|b|c",
            (a | b | c).ToString()
        );
        Assert.AreEqual(
            "a(b|c)",
            (a & (b | c)).ToString()
        );
    }
}

