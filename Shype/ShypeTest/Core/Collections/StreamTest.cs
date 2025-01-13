using Shype.Core.Errors;

namespace Shype.Core.Collections;

public class StreamTest
{
    private record TestStream : Stream<TestStream, int>
    {
        public TestStream(params int[] items) : base(items) { }

        public new TestStream Tail { get => new() { Items = base.Tail.Items }; }
    }

    [Test]
    public void TestEquals()
    {
        Assert.That(
            new TestStream { Items = { } }, Is.EqualTo(
            new TestStream())
        );
        Assert.That(
            new TestStream(1),
            Is.EqualTo(new TestStream(1))
        );
        Assert.That(
            new TestStream(1),
            Is.Not.EqualTo(new TestStream())
        );
        Assert.That(
            new TestStream(1),
            Is.Not.EqualTo(new TestStream(2))
        );
    }

    [Test]
    public void TestHead()
    {
        Assert.Catch<Error>(() => { _ = new TestStream().Head; });
        Assert.That(new TestStream(1).Head, Is.EqualTo(1));
        Assert.That(new TestStream(1, 2).Head, Is.EqualTo(1));
    }

    [Test]
    public void TestTail()
    {
        Assert.Catch<Error>(() => { _ = new TestStream().Tail; });
        Assert.That(new TestStream(1).Tail, Is.EqualTo(new TestStream()));
        Assert.That(new TestStream(1, 2).Tail, Is.EqualTo(new TestStream(2)));
    }
}