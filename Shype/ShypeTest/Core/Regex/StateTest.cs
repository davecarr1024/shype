namespace Shype.Core.Regex;

[TestClass]
public class StateTest
{
    [TestMethod]
    public void TestFromString()
    {
        Assert.AreEqual(
            new State(
                new Chars.Char('a'),
                new Chars.Char('b', new(0, 1))
            ),
            new State("ab")
        );
        Assert.AreEqual(
            new State(
                new Chars.Char('a', new(5, 6)),
                new Chars.Char('b', new(5, 7))
            ),
            new State("ab", new(5, 6))
        );
    }

    [TestMethod]
    public void TestHead()
    {
        Assert.ThrowsException<State.Error>(new State().Head);
        Assert.AreEqual(
            new Chars.Char('a'),
            new State("ab").Head()
        );
    }

    [TestMethod]
    public void TestTail()
    {
        Assert.ThrowsException<State.Error>(new State().Tail);
        Assert.AreEqual(
            new State(),
            new State("a").Tail()
        );
        Assert.AreEqual(
            new State("b", new(0, 1)),
            new State("ab").Tail()
        );
    }
}