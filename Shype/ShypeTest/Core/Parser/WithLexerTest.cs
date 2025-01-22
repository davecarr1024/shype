namespace Shype.Core.Parser;

[TestClass]
public class WithLexerTest
{
    [TestMethod]
    public void TestApply()
    {
        Assert.AreEqual(
            123,
            Parser
                .Token("int", Regex.Regex.Digits().OneOrMore())
                .Value()
                .Transform(int.Parse)
                .IgnoreWhitespace()
                .Apply("\t 123\n")
                .result
        );
    }
}