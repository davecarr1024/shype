namespace Shype.Core.Streams;

public class StreamTest
{
    private record TestStream(IImmutableList<int> Items) : Stream<TestStream, int>(Items)
    {
        public TestStream() : this(ImmutableList<int>.Empty) { }

        public TestStream(params int[] items) : this(items.ToImmutableList()) { }
    }

    [Test]
    public void TestEqual()
    {
        CollectionAssert.AreEqual(new TestStream(), new TestStream());
        CollectionAssert.AreEqual(new TestStream(1), new TestStream(1));
        CollectionAssert.AreEqual(new TestStream(1, 2), new TestStream(1, 2));
        CollectionAssert.AreNotEqual(new TestStream(1), new TestStream());
        CollectionAssert.AreNotEqual(new TestStream(1), new TestStream(2));
    }

    [Test]
    public void TestHead()
    {
        Assert.Catch<Errors.Error>(() => { var _ = new TestStream().Head; });
        Assert.That(
            new TestStream(1).Head,
            Is.EqualTo(1)
        );
        Assert.That(
            new TestStream(1, 2).Head,
            Is.EqualTo(1)
        );
    }

    [Test]
    public void TestTail()
    {
        Assert.Catch<Errors.Error>(() => { var _ = new TestStream().Tail; });
        CollectionAssert.AreEqual(
            new TestStream(1).Tail,
            new TestStream()
        );
        CollectionAssert.AreEqual(
            new TestStream(1, 2).Tail,
            new TestStream(2)
        );
    }

    [Test]
    public void TestAdd()
    {
        CollectionAssert.AreEqual(
            new TestStream(1, 2),
            new TestStream(1) + new TestStream(2)
        );
        CollectionAssert.AreEqual(
            new TestStream(1, 2),
            new TestStream(1) + 2
        );
        CollectionAssert.AreEqual(
            new TestStream(1, 2),
            1 + new TestStream(2)
        );
    }
}