namespace Shype.Core.Errors;

public class ErrorableTest
{
    private record PositiveInt : Errorable<PositiveInt>
    {
        public PositiveInt(int value = 0) => Value = value;

        private int value;

        public int Value
        {
            set
            {
                if (value < 0)
                {
                    throw CreateError("negative value");
                }
                this.value = value;
            }
            get => value;
        }

        public int SetAndGet(int value)
        {
            return Try(() => { Value = value; return Value; });
        }
    }

    [Test]
    public void TestCtor()
    {
        new PositiveInt(0);
        new PositiveInt(1);
        Assert.Catch<Error>(() => { new PositiveInt(-1); });
    }

    [Test]
    public void TestSetterThrows()
    {
        PositiveInt i = new();
        Assert.That(
            Assert.Catch<Error>(() => { i.Value = -1; }),
            Is.EqualTo(new Error("negative value"))
        );
    }

    [Test]
    public void TestTry()
    {
        PositiveInt i = new();
        Assert.That(i.SetAndGet(1), Is.EqualTo(1));
        Assert.Catch<Error>(() => { i.SetAndGet(-1); });
    }
}