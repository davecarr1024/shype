namespace Shype.Core.Parser;

[TestClass]
public class ObjectTest
{
    private record Obj(int I = 0, string S = "");

    [TestMethod]
    public void TestApply()
    {
        Assert.AreEqual(
            new Obj(1, "a"),
            new Object<Obj>([
                new Arg<Obj,int>((obj, i) => obj with {I = i}, 1),
                new Arg<Obj,string>((obj, s) => obj with {S = s}, "a")
            ]).Apply(new())
        );
    }

    [TestMethod]
    public void TestParserApply()
    {
        Parser<Arg<Obj>> i =
            Parser
                .Token("i")
                .Value()
                .Transform(int.Parse)
                .Arg<Obj>((obj, i) => obj with { I = i });
        Parser<Arg<Obj>> s =
            Parser
                .Token("s")
                .Value()
                .Arg<Obj>((obj, s) => obj with { S = s });
        Parser<Obj> obj = (i & s).Object<Obj>(new());
        Assert.AreEqual(
            (new State(), new Obj(1, "a")),
            obj.Apply(new State(
                new Tokens.Token("i", "1"),
                new Tokens.Token("s", "a")
            ))
        );
    }
}