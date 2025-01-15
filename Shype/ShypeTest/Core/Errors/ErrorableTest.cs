namespace Shype.Core.Errors;

[TestClass]
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

    [TestMethod]
    public void TestCtor()
    {
        var _ = new PositiveInt(0);
        _ = new PositiveInt(1);
        Assert.ThrowsException<PositiveInt.Error>(() => new PositiveInt(-1));
    }

    [TestMethod]
    public void TestSetterThrows()
    {
        PositiveInt i = new();
        Assert.AreEqual(
            new PositiveInt.Error(i, [], "negative value"),
            Assert.ThrowsException<PositiveInt.Error>(() => { i.Value = -1; })
        );
    }

    [TestMethod]
    public void TestTry()
    {
        PositiveInt i = new();
        Assert.AreEqual(1, i.SetAndGet(1));
        Assert.AreEqual(
            new PositiveInt.Error(i, [new PositiveInt.Error(i, [], "negative value")]),
            Assert.ThrowsException<PositiveInt.Error>(() => i.SetAndGet(-1))
        );
    }
}