namespace Shype.Core.Regex;

[TestClass]
public class AnyTest()
{
    [TestMethod]
    public void TestApply()
    {
        Assert.ThrowsException<Regex.Error>(() => Regex.Any().Apply(""));
        Assert.AreEqual(
            (new State(), new Result(new Chars.Char('a'))),
            Regex.Any().Apply("a")
        );
        Assert.AreEqual(
            (new State(new Chars.Char('b', new(0, 1))), new Result(new Chars.Char('a'))),
            Regex.Any().Apply("ab")
        );
    }

    [TestMethod]
    public void TestToString()
    {
        Assert.AreEqual(".", Regex.Any().ToString());
    }
}