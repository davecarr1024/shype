namespace Shype.Core.Parser;

[TestClass]
public class ObjectTest
{
    private record Obj(int I = 0, string S = "")
    {
        public static Obj ISetter(Obj o, int i) => o with { I = i };

        public static Obj SSetter(Obj o, string s) => o with { S = s };
    }

    [TestMethod]
    public void TestApply()
    {
        Assert.AreEqual(
            new Obj(1, "a"),
            (
                Parser
                    .Token("i")
                    .Value()
                    .Transform(int.Parse)
                    .Arg<Obj>(Obj.ISetter)
                & Parser
                    .Token("s")
                    .Value()
                    .Arg<Obj>(Obj.SSetter)
            ).Object(new())
                .Apply(new State([
                    new Tokens.Token("i", "1"),
                    new Tokens.Token("s", "a")
                ])).result
        );
    }
}