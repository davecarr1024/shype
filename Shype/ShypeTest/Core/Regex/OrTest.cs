namespace Shype.Core.Regex;

[TestClass]
public class OrTest
{
    [TestMethod]
    public void TestOr()
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
}

