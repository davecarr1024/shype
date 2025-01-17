namespace Shype.Core.Regex;

[TestClass]
public class NotTest
{
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

