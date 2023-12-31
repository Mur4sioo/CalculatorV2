using System.Globalization;
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
            Assert.AreEqual(result, actual);
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
            var tokens = CalculatorEngine.Tokenization("1+(2-4)");
            List<Token> result = new List<Token>();
            result.Add(new Token(TokenType.Number, 1));
            result.Add(new Token(TokenType.OperatorPlus, 0));
            result.Add(new Token(TokenType.ParenOpen, 0));
            result.Add(new Token(TokenType.Number, 2));
            result.Add(new Token(TokenType.OperatorMinus, 0));
            result.Add(new Token(TokenType.Number, 4));
            result.Add(new Token(TokenType.ParenClose, 0));
            CollectionAssert.AreEqual(result, tokens);
        }

        [TestMethod]
        public void TestAdditive()
        {
            var actual = Parser.ParseExpression("1,2 * 2") as BinaryNode;
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Left is NumberNode);
            Assert.IsTrue(actual.Right is NumberNode);
            Assert.AreEqual(BinaryOperator.Multiply, actual.Operator);
            Assert.AreEqual(1.2d, (actual.Left as NumberNode).Value);
            Assert.AreEqual(2d, (actual.Right as NumberNode).Value);
        }

        [TestMethod]
        public void TestEvaluate()
        {
            var math = engine.Evaluate("1 + (2 +3) *2 + 1");
            double actual = 12;
            Assert.AreEqual(actual, math);
        }

        [TestMethod]
        public void TestNegation()
        {
            var math = engine.Evaluate("-5--7");
            double actual = 2;
            Assert.AreEqual(actual, math);
        }

        [TestMethod]
        public void TestNegation2()
        {
            var math = engine.Evaluate("-5----7");
            double actual = 2;
            Assert.AreEqual(actual, math);
        }

        [TestMethod]
        public void TestEnUsCulture()
        {
            var culture = CultureInfo.GetCultureInfo("en-US");
            string math = "1.2";
            var actual = engine.Evaluate(math, culture);
            Assert.AreEqual(1.2d, actual);
        }

        [TestMethod]
        public void TestFrFrCulture()
        {
            var culture = CultureInfo.GetCultureInfo("fr-FR");
            string math = "1,2";
            var actual = engine.Evaluate(math, culture);
            Assert.AreEqual(1.2d, actual);
        }

        [TestMethod]
        public void TestCurrentCulture()
        {
            var culture = CultureInfo.CurrentCulture;
            string math = "1" + culture.NumberFormat.NumberDecimalSeparator + "2";
            var actual = engine.Evaluate(math, culture);
            Assert.AreEqual(1.2d, actual);
        }

        [TestMethod]
        public void TestIdentifier()
        {
            var tokens = CalculatorEngine.Tokenization("1+(x-4)");
            List<Token> result = new List<Token>();
            result.Add(new Token(TokenType.Number, 1, string.Empty));
            result.Add(new Token(TokenType.OperatorPlus, 0, string.Empty));
            result.Add(new Token(TokenType.ParenOpen, 0, string.Empty));
            result.Add(new Token(TokenType.Identifier, 0, "x"));
            result.Add(new Token(TokenType.OperatorMinus, 0, string.Empty));
            result.Add(new Token(TokenType.Number, 4, string.Empty));
            result.Add(new Token(TokenType.ParenClose, 0, string.Empty));
            CollectionAssert.AreEqual(result, tokens);  
        }

        [TestMethod]
        public void TestVariables()
        {
            var variables = new Dictionary<string, double>();
            variables.Add("x", 1);
            variables.Add("y", 2);      
            Assert.AreEqual(3, engine.Evaluate("x+y", variables));
        }
    }
}