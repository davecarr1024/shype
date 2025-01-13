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
        new PositiveInt(0);
        new PositiveInt(1);
        Assert.ThrowsException<PositiveInt.Error>(() => { new PositiveInt(-1); });
    }

    [TestMethod]
    public void TestSetterThrows()
    {
        PositiveInt i = new();
        PositiveInt.Error error = Assert.ThrowsException<PositiveInt.Error>(() => { i.Value = -1; });
        Assert.AreSame(i, error.Object);
        Assert.AreEqual("negative value", error.Message);
    }

    [TestMethod]
    public void TestTry()
    {
        PositiveInt i = new();
        Assert.AreEqual(1, i.SetAndGet(1));
        PositiveInt.UnaryError error = Assert.ThrowsException<PositiveInt.UnaryError>(() => { i.SetAndGet(-1); });
        Assert.AreSame(i, error.Object);
        Assert.AreSame(i, error.Child.Object);
        Assert.AreEqual("negative value", error.Child.Message);
    }
}