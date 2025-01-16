namespace Shype.Core.Lexer;

[TestClass]
public class ResultTest
{
    [TestMethod]
    public void TestEqual()
    {
        Assert.AreEqual(
            new Result(),
            new Result()
        );
        Assert.AreEqual(
            new Result(new Tokens.Token("r", "a", new(0, 0))),
            new Result(new Tokens.Token("r", "a", new(0, 0)))
        );
        Assert.AreNotEqual(
            new Result(),
            new Result(new Tokens.Token("r", "a", new(0, 0)))
        );
        Assert.AreNotEqual(
            new Result(new Tokens.Token("r", "a", new(0, 0))),
            new Result(new Tokens.Token("s", "a", new(0, 0)))
        );
        Assert.AreNotEqual(
            new Result(new Tokens.Token("r", "a", new(0, 0))),
            new Result(new Tokens.Token("r", "b", new(0, 0)))
        );
        Assert.AreNotEqual(
            new Result(new Tokens.Token("r", "a", new(0, 0))),
            new Result(new Tokens.Token("r", "a", new(1, 2)))
        );
    }

    [TestMethod]
    public void TestAdd()
    {
        Assert.AreEqual(
            new Result(new("r", "a", new(0, 0)), new("s", "b", new(0, 1))),
            new Result(new Tokens.Token("r", "a", new(0, 0))) + new Result(new Tokens.Token("s", "b", new(0, 1)))
        );
        Assert.AreEqual(
            new Result(new("r", "a", new(0, 0)), new("s", "b", new(0, 1))),
            new Result(new Tokens.Token("r", "a", new(0, 0))) + new Tokens.Token("s", "b", new(0, 1))
        );
        Assert.AreEqual(
            new Result(new("r", "a", new(0, 0)), new("s", "b", new(0, 1))),
            new Tokens.Token("r", "a", new(0, 0)) + new Result(new Tokens.Token("s", "b", new(0, 1)))
        );
    }
}