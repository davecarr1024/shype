namespace Shype.Core.Streams;

[TestClass]
public class StreamTest
{
    private record TestStream(IImmutableList<int> Items)
        : Stream<TestStream, int>(Items)
    {
        public TestStream(params int[] items) : this(items.ToImmutableList()) { }

        public override string ToString() => base.ToString();
    }

    [TestMethod]
    public void TestEquals()
    {
        Assert.AreEqual(new TestStream(), new TestStream());
        Assert.AreEqual(
            new TestStream(1),
            new TestStream(1)
        );
        Assert.AreNotEqual(
            new TestStream(),
            new TestStream(1)
        );
        Assert.AreNotEqual(
            new TestStream(1),
            new TestStream(2)
        );
    }

    [TestMethod]
    public void TestEmpty()
    {
        Assert.IsTrue(new TestStream().Empty());
        Assert.IsFalse(new TestStream(1).Empty());
    }

    [TestMethod]
    public void TestHead()
    {
        Assert.ThrowsException<TestStream.Error>(() => new TestStream().Head());
        Assert.AreEqual(
            1,
            new TestStream(1).Head()
        );
    }

    [TestMethod]
    public void TestTail()
    {
        Assert.ThrowsException<TestStream.Error>(new TestStream().Tail);
        Assert.AreEqual(
            new TestStream(),
            new TestStream(1).Tail()
        );
        Assert.AreEqual(
            new TestStream(2),
            new TestStream(1, 2).Tail()
        );
    }

    [TestMethod]
    public void TestAdd()
    {
        Assert.AreEqual(new TestStream(), new TestStream() + new TestStream());
        Assert.AreEqual(
            new TestStream(1, 2),
            new TestStream(1) + new TestStream(2)
        );
        Assert.AreEqual(
            new TestStream(1, 2),
            new TestStream(1) + 2
        );
        Assert.AreEqual(
            new TestStream(1, 2),
            1 + new TestStream(2)
        );
    }
}