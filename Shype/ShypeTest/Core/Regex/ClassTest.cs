namespace Shype.Core.Regex;

[TestClass]
public class ClassTest
{
    [TestMethod]
    public void TestClass()
    {
        Regex regex = Regex.Digits();
        Assert.ThrowsException<Regex.Error>(() => regex.Apply(""));
        Assert.ThrowsException<Regex.Error>(() => regex.Apply("a"));
        Assert.AreEqual(
            (new State(), new Result("0")),
            regex.Apply("0")
        );
        Assert.AreEqual(
            (new State("b", new(0, 1)), new Result("0")),
            regex.Apply("0b")
        );
        Assert.AreEqual("\\d", regex.ToString());
    }
}

