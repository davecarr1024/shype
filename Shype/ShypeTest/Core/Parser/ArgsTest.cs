namespace Shype.Core.Parser;

[TestClass]
public class ArgsTest
{
    private record Obj(int I);

    [TestMethod]
    public void TestCombine()
    {
        Arg<Obj> a
            = Parser
                .Token("a", Regex.Regex.Digits())
                .Value()
                .Transform(int.Parse)
                .Arg((Obj obj, int i) => (obj with { I = i }));
        Arg<Obj> b
            = Parser
                .Token("b", Regex.Regex.Digits())
                .Value()
                .Transform(int.Parse)
                .Arg((Obj obj, int i) => (obj with { I = i }));
        Arg<Obj> c
            = Parser
                .Token("c", Regex.Regex.Digits())
                .Value()
                .Transform(int.Parse)
                .Arg((Obj obj, int i) => (obj with { I = i }));
        Arg<Obj> d
            = Parser
                .Token("d", Regex.Regex.Digits())
                .Value()
                .Transform(int.Parse)
                .Arg((Obj obj, int i) => (obj with { I = i }));
        Assert.AreEqual(
            new Args<Obj>([a, b, c, d]),
            a & b & c & d
        );
        Assert.AreEqual(
            new Args<Obj>([a, b, c, d]),
            a & (b & c) & d
        );
        Assert.AreEqual(
            new Args<Obj>([a, b, c, d]),
            a & b & (c & d)
        );
        Assert.AreEqual(
            new Args<Obj>([a, b, c, d]),
            "(" & (a & b & c & d)
        );
        Assert.AreEqual(
            new Args<Obj>([a, b, c, d]),
            a & b & c & d & ")"
        );
    }
}