using Evaluator;

namespace TestProject4
{
    [TestClass]
    public class UnitTest1
    {
        CalculatorEngine engine = new CalculatorEngine();
        [TestMethod]
        public void TestMethod1()
        {
            string math = "1+2";
            string result = "3";
            string actual = engine.Evaluate(math).ToString();
            Assert.AreEqual(result,actual);
        }
        [TestMethod]
        public void TestMethod2()
        {
            string math = "2-1";
            string result = "1";
            string actual = engine.Evaluate(math).ToString();
            Assert.AreEqual(result, actual);
        }
        [TestMethod]
        public void TestTokenization()
        {
            var tokens = CalculatorEngine.Tokenization("1+2-4");
            List<Token> result = new List<Token>();
            result.Add(new Token(TokenType.Number, 1));
            result.Add(new Token(TokenType.OperatorPlus, 0));
            result.Add(new Token(TokenType.Number, 2));
            result.Add(new Token(TokenType.OperatorMinus, 0));
            result.Add(new Token(TokenType.Number, 4));
            CollectionAssert.AreEqual(result, tokens);
        }
        [TestMethod]
        public void TestAdditive()
        {
            var actual = Parser.ParseExpression("1 * 2") as BinaryNode;
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Left is NumberNode);
            Assert.IsTrue(actual.Right is NumberNode);
            Assert.AreEqual(BinaryOperator.Add, actual.Operator);
            Assert.AreEqual(1d, (actual.Left as NumberNode).Value);
            Assert.AreEqual(2d, (actual.Right as NumberNode).Value);
        }
    }
}