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

    [TestMethod]
    public void TestMessage()
    {
        Assert.AreEqual(
            "a",
            new Error("a").Message
        );
        Assert.AreEqual(
            "a\n  b",
            new Error("a", new Error("b")).Message
        );
        Assert.AreEqual(
            "a\n  b\n  c",
            new Error("a", new Error("b"), new Error("c")).Message
        );
        Assert.AreEqual(
            "a\n  b\n    c",
            new Error("a", new Error("b", new Error("c"))).Message
        );
        Assert.AreEqual(
            "a\n  b\n    c\n  d\n    e",
            new Error("a",
                new Error("b",
                    new Error("c")
                ),
                new Error("d",
                    new Error("e")
                )
            ).Message
        );
    }
}