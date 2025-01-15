namespace Shype.Core.Regex;

[TestClass]
public class ResultTest
{
    [TestMethod]
    public void TestEqual()
    {
        Assert.AreEqual(new Result(), new Result());
        Assert.AreEqual(
            new Result(
                new Chars.Char('a'),
                new Chars.Char('b', new(0, 1))
            ),
            new Result("ab")
        );
    }

    [TestMethod]
    public void TestValue()
    {
        Assert.AreEqual("", new Result().Value());
        Assert.AreEqual("ab", new Result("ab").Value());
    }

    [TestMethod]
    public void TestPosition()
    {
        Assert.ThrowsException<Result.Error>(new Result().Position);
        Assert.AreEqual(new Result("ab").Position(), new Chars.Position(0, 0));
        Assert.AreEqual(new Result("ab", new(1, 2)).Position(), new Chars.Position(1, 2));
    }

    [TestMethod]
    public void TestAdd()
    {
        Assert.AreEqual(
            new Result("ab"),
            new Result("a") + new Result("b", new(0, 1))
        );
        Assert.AreEqual(
            new Result("ab"),
            new Result("a") + new Chars.Char('b', new(0, 1))
        );
        Assert.AreEqual(
            new Result("ab"),
            new Chars.Char('a') + new Result("b", new(0, 1))
        );
    }
}