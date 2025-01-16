namespace Shype.Core.Errors;


[TestClass]
public class ErrorTest
{
    [TestMethod]
    public void TestEqual()
    {
        Assert.AreEqual(
            new Error("a"),
            new Error("a")
        );
        Assert.AreNotEqual(
            new Error("a"),
            new Error("b")
        );
        Assert.AreNotEqual(
            new Error("a"),
            new Error("a", new Error("b"))
        );
    }
}