namespace Shype.Core.Chars;

[TestClass]
public class StreamTest
{
    [TestMethod]
    public void TestEquals()
    {
        Assert.AreEqual(new Stream(), new Stream());
        Assert.AreEqual(
            new Stream(new Char('a', new(0, 0))),
            new Stream(new Char('a', new(0, 0)))
        );
        Assert.AreNotEqual(
            new Stream(),
            new Stream(new Char('a', new(0, 0)))
        );
        Assert.AreNotEqual(
            new Stream(new Char('a', new(0, 0))),
            new Stream(new Char('b', new(0, 0)))
        );
        Assert.AreNotEqual(
            new Stream(new Char('a', new(0, 0))),
            new Stream(new Char('a', new(1, 2)))
        );
    }

    [TestMethod]
    public void TestFromString()
    {
        Assert.AreEqual(
            new Stream(),
            new Stream("")
        );
        Assert.AreEqual(
            new Stream(
                new Char('a', new(0, 0)),
                new Char('\n', new(0, 1)),
                new Char('b', new(1, 0))
            ),
            new Stream("a\nb")
        );
        Assert.AreEqual(
            new Stream(
                new Char('a', new(5, 6)),
                new Char('\n', new(5, 7)),
                new Char('b', new(6, 0))
            ),
            new Stream("a\nb", new(5, 6))
        );
    }

    [TestMethod]
    public void TestEmpty()
    {
        Assert.IsTrue(new Stream().Empty());
        Assert.IsFalse(new Stream("a").Empty());
    }

    [TestMethod]
    public void TestHead()
    {
        Assert.ThrowsException<Stream.Error>(new Stream().Head);
        Assert.AreEqual(
            new Char('a', new(0, 0)),
            new Stream(new Char('a', new(0, 0))).Head()
        );
    }

    [TestMethod]
    public void TestTail()
    {
        Assert.ThrowsException<Stream.Error>(new Stream().Tail);
        Assert.AreEqual(
            new Stream(),
            new Stream(new Char('a', new(0, 0))).Tail()
        );
        Assert.AreEqual(
            new Stream(new Char('b', new(0, 1))),
            new Stream(
                new Char('a', new(0, 0)),
                new Char('b', new(0, 1))
            ).Tail()
        );
    }

    [TestMethod]
    public void TestPosition()
    {
        Assert.ThrowsException<Stream.Error>(new Stream().Position);
        Assert.AreEqual(
            new Stream("a").Position(),
            new Position(0, 0)
        );
        Assert.AreEqual(
            new Stream("a", new(1, 2)).Position(),
            new Position(1, 2)
        );
    }

    [TestMethod]
    public void TestValue()
    {
        Assert.AreEqual("", new Stream().Value());
        Assert.AreEqual("ab", new Stream("ab").Value());
        Assert.AreEqual("ab", new Stream("ab", new(1, 2)).Value());
    }

    [TestMethod]
    public void TestAdd()
    {
        Assert.AreEqual(new Stream(), new Stream() + new Stream());
        Assert.AreEqual(
            new Stream("ab"),
            new Stream("a") + new Stream("b", new(0, 1))
        );
        Assert.AreEqual(
            new Stream("ab"),
            new Stream("a") + new Char('b', new(0, 1))
        );
        Assert.AreEqual(
            new Stream("ab"),
            new Char('a', new(0, 0)) + new Stream("b", new(0, 1))
        );
    }
}