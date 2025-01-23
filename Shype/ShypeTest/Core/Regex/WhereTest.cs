namespace Shype.Core.Regex;

[TestClass]
public class WhereTest
{
    [TestMethod]
    public void TestApply()
    {
        Regex regex = (Regex.Literal('a') | Regex.Literal('b')).Except(["b"]);
        Assert.AreEqual(
            (new State(), new Result("a")),
            regex.Apply("a")
        );
        Assert.ThrowsException<Regex.Error>(() => regex.Apply("b"));
    }
}