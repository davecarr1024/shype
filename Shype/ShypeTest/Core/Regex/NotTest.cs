namespace Shype.Core.Regex;

[TestClass]
public class NotTest
{
    [TestMethod]
    public void TestNot()
    {
        Regex regex = Regex.Literal('a').Not();
        Assert.ThrowsException<Regex.Error>(() => regex.Apply(""));
        Assert.ThrowsException<Regex.Error>(() => regex.Apply("a"));
        Assert.AreEqual(
            (new State(), new Result("b")),
            regex.Apply("b")
        );
    }

    [TestMethod]
    public void TestToString()
    {
        Assert.AreEqual("^a", Regex.Literal('a').Not().ToString());
    }
}

