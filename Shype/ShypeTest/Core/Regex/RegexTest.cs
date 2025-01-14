namespace Shype.Core.Regex;

public class RegexTest
{
    [Test]
    public void TestLiteral()
    {
        Assert.That(
            Regex.Literal('a').Apply(new State("a")),
            Is.EqualTo((new State(), new Result("a")))
        );
    }
}