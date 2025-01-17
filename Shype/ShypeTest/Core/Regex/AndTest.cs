namespace Shype.Core.Regex;

[TestClass]
public class AndTest
{
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
            () => regex.Apply("b")
        );
        Assert.ThrowsException<Regex.Error>(
            () => regex.Apply(new("c"))
        );
    }
}

