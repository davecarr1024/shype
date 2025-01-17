namespace Shype.Core.Regex;

[TestClass]
public class RangeTest
{
    [TestMethod]
    public void TestApply()
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
    public void TestToString()
    {
        Assert.AreEqual(
            "[a-z]",
            Regex.Range('a', 'z').ToString()
        );
    }
}

