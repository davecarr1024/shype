namespace Shype.Core.Parser;

[TestClass]
public class ArgTest
{
    private record Int(int Value = 0);

    [TestMethod]
    public void TestApplySetter()
    {
        Assert.AreEqual(
            new Int(1),
            new Arg<Int, int>.Setter((obj, value) => obj with { Value = value }, 1).Apply(new())
        );
    }

    [TestMethod]
    public void TestApply()
    {
        Arg<Int> i =
            Parser
                .Token("i", Regex.Regex.Digits())
                .Value()
                .Transform(int.Parse)
                .Arg((Int obj, int value) => obj with { Value = value });
        Assert.AreEqual(
            new Int(1),
            i.Apply("1").result.Apply(new())
        );
    }
}