namespace Shype.Core.Lexer;

[TestClass]
public class LexerTest
{
    [TestMethod]
    public void TestApply()
    {
        Lexer lexer = new(
            new("r", "a"),
            new("s", "b"),
            new("ws", Regex.Regex.Whitespace(), false)
        );
        Assert.ThrowsException<Lexer.Error>(() => lexer.Apply("c"));
        Assert.AreEqual(
            new Result(),
            lexer.Apply("")
        );
        Assert.AreEqual(
            new Result(new Tokens.Token("r", "a", new(0, 0))),
            lexer.Apply("a")
        );
        Assert.AreEqual(
            new Result(new Tokens.Token("s", "b", new(0, 0))),
            lexer.Apply("b")
        );
        Assert.AreEqual(
            new Result(
                new Tokens.Token("r", "a", new(0, 0)),
                new Tokens.Token("s", "b", new(0, 1))
            ),
            lexer.Apply("ab")
        );
        Assert.AreEqual(
            new Result(
                new Tokens.Token("r", "a", new(1, 0)),
                new Tokens.Token("s", "b", new(1, 2))
            ),
            lexer.Apply("\na b\t")
        );
    }

    [TestMethod]
    public void TestAdd()
    {
        Assert.AreEqual(
            new Lexer(new("r", "a"), new("s", "b")),
            new Lexer(new Rule("r", "a")) + new Lexer(new Rule("s", "b"))
        );
        Assert.AreEqual(
            new Lexer(new("r", "a"), new("s", "b")),
            new Lexer(new Rule("r", "a")) + new Rule("s", "b")
        );
        Assert.AreEqual(
            new Lexer(new("r", "a"), new("s", "b")),
            new Rule("r", "a") + new Lexer(new Rule("s", "b"))
        );
    }
}