namespace Shype.Core.Regex;

[TestClass]
public class ZeroOrOneTest()
{
    [TestMethod]
    public void TestApply()
    {
        ZeroOrOne regex = Regex.Literal('a').ZeroOrOne();
        Assert.AreEqual(
            (new State(), new Result()),
            regex.Apply("")
        );
        Assert.AreEqual(
            (new State(), new Result(new Chars.Char('a'))),
            regex.Apply("a")
        );
        Assert.AreEqual(
            (new State(new Chars.Char('b', new(0, 1))), new Result(new Chars.Char('a'))),
            regex.Apply("ab")
        );
    }

    [TestMethod]
    public void TestToString()
    {
        Assert.AreEqual("a?", Regex.Literal('a').ZeroOrOne().ToString());
    }
}