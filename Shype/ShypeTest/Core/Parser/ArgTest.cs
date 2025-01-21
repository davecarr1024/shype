namespace Shype.Core.Parser;

[TestClass]
public class ArgTest
{
    private record Int(int Value = 0);

    [TestMethod]
    public void TestApply()
    {
        Assert.AreEqual(
            new Int(1),
            new Arg<Int, int>((obj, value) => obj with { Value = value }, 1).Apply(new())
        );
    }
}