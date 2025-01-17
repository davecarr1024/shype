namespace Shype.Core.Regex;

[TestClass]
public class AndTest
{
    [TestMethod]
    public void TestApply()
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
            () => regex.Apply("b")
        );
        Assert.ThrowsException<Regex.Error>(
            () => regex.Apply(new("c"))
        );
    }

    [TestMethod]
    public void TestCombine()
    {
        Literal a = new('a'), b = new('b'), c = new('c'), d = new('d');
        Assert.AreEqual(a, Regex.And(a));
        Assert.AreEqual(
            Regex.And(a, b, c),
            a & b & c
        );
        Assert.AreEqual(
            Regex.And(a, b, c),
            a & (b & c)
        );
        Assert.AreEqual(
            Regex.And(a, b, c, d),
            a & b & (c & d)
        );
    }

    [TestMethod]
    public void TestToString()
    {
        Literal a = new('a'), b = new('b'), c = new('c');
        Assert.AreEqual(
            "abc",
            (a & b & c).ToString()
        );
        Assert.AreEqual(
            "a|(bc)",
            (a | (b & c)).ToString()
        );
    }
}

