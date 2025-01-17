namespace Shype.Core.Regex;

[TestClass]
public class ZeroOrOneTest
{
    [TestMethod]
    public void TestZeroOrOne()
    {
        Regex regex = Regex.Literal('a').ZeroOrOne();
        Assert.AreEqual(
            (new State(), new Result()),
            regex.Apply(new())
        );
        Assert.AreEqual(
            (new State("b"), new Result()),
            Regex.Literal('a').ZeroOrMore().Apply("b")
        );
        Assert.AreEqual(
            (new State("b", new(0, 1)), new Result("a")),
            Regex.Literal('a').ZeroOrOne().Apply(new("ab"))
        );
        Assert.AreEqual(
            (new State(), new Result("a")),
            Regex.Literal('a').ZeroOrOne().Apply(new("a"))
        );
    }
}

