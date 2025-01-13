namespace Shype.Core.Errors;


public class ErrorTest
{
    [Test]
    public void TestEqual()
    {
#pragma warning disable NUnit2009
        Assert.That(new Error("a"), Is.EqualTo(new Error("a")));
#pragma warning restore NUnit2009
    }

}