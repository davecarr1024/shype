namespace Shype.Core.Regex;

public class StateTest
{
    [Test]
    public void TestEqual()
    {
        CollectionAssert.AreEqual(new State(), new State());
        CollectionAssert.AreEqual(new State("a"), new State("a"));
        Assert.That(new State("a"), Is.EqualTo(new State("a")));
    }

    [Test]
    public void TestFromString()
    {
        CollectionAssert.AreEqual(
            new State("a\nb"),
            new State(ImmutableList.Create<Chars.Char>(
                new Chars.Char('a', new(0, 0)),
                new Chars.Char('\n', new(0, 1)),
                new Chars.Char('b', new(1, 0))
            ))
        );
        CollectionAssert.AreEqual(
             new State("a\nb", new(5, 5)),
             new State(ImmutableList.Create<Chars.Char>(
                 new Chars.Char('a', new(5, 5)),
                 new Chars.Char('\n', new(5, 6)),
                 new Chars.Char('b', new(6, 0))
             ))
         );
    }
}