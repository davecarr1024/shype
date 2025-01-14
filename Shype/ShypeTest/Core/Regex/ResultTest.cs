namespace Shype.Core.Regex;

public class ResultTest
{
    [Test]
    public void TestEqual()
    {
        CollectionAssert.AreEqual(
            new Result(),
            new Result()
        );
        CollectionAssert.AreEqual(
            new Result(new Chars.Char('a', new(0, 0))),
            new Result(new Chars.Char('a', new(0, 0)))
        );
        CollectionAssert.AreEqual(
            new Result("a"),
            new Result("a")
        );
        CollectionAssert.AreNotEqual(
            new Result("a"),
            new Result("b")
        );
        CollectionAssert.AreNotEqual(
            new Result("a"),
            new Result("a", new(1, 2))
        );
    }

    [Test]
    public void TestTupleEqual()
    {
        Assert.That(
            (new State(), new Result()),
            Is.EqualTo((new State(), new Result()))
        );
        Assert.That(new State("a"), Is.EqualTo(new State("a")));
        Assert.That(new Result("b"), Is.EqualTo(new Result("b")));
        Assert.That(
            (new State("a"), new Result("b")),
            Is.EqualTo((new State("a"), new Result("b")))
        );
    }

    [Test]
    public void TestPosition()
    {
        Assert.Catch<Errors.Error>(() => { var _ = new Result().Position; });
        Assert.That(
            new Result(new Chars.Char('a', new(1, 2))).Position,
            Is.EqualTo(new Chars.Position(1, 2))
        );
    }

    [Test]
    public void TestValue()
    {
        Assert.That(
            new Result().Value,
            Is.EqualTo("")
        );
        Assert.That(
            new Result(new Chars.Char('a', new(1, 2))).Value,
            Is.EqualTo("a")
        );
        Assert.That(
            new Result(
                new Chars.Char('a', new(1, 2)),
                new Chars.Char('b', new(1, 2))
            ).Value,
            Is.EqualTo("ab")
        );
    }
}