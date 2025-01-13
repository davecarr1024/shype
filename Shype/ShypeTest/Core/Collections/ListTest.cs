namespace Shype.Core.Collections;

public class ListTest {
    [Test]
    public void TestEquals() {
        Assert.That(
            new List<int>(),
            Is.EqualTo(new List<int>())
        );
        Assert.That(
            new List<int>(1),
            Is.EqualTo(new List<int>(1))
        );
        Assert.That(
            new List<int>(1),
            Is.Not.EqualTo(new List<int>())
        );
        Assert.That(
            new List<int>(1),
            Is.Not.EqualTo(new List<int>(2))
        );
    }
}