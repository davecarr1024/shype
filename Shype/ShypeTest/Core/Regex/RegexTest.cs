namespace Shype.Core.Regex;

[TestClass]
public class RegexTest
{
    [TestMethod]
    public void TestEqual()
    {
        Assert.AreEqual(Regex.Literal('a'), Regex.Literal('a'));
        Assert.AreNotEqual(Regex.Literal('a'), Regex.Literal('b'));
        Assert.AreEqual(
            Regex.And(Regex.Literal('a')),
            Regex.And(Regex.Literal('a'))
        );
        Assert.AreNotEqual(
            Regex.And(),
            Regex.And(Regex.Literal('a'))
        );
        Assert.AreNotEqual(
            Regex.And(Regex.Literal('a')),
            Regex.And(Regex.Literal('b'))
        );
    }

    [TestMethod]
    public void TestCreate()
    {
        Assert.AreEqual(
            Regex.And(Regex.Literal('a'), Regex.Literal('b'), Regex.Literal('c')),
            Regex.Create("abc")
        );
    }
}
