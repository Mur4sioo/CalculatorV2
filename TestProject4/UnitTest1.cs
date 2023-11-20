using Calculator;

namespace TestProject4
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            CalculatorEngine engine = new CalculatorEngine();
            string math = "4 * 2 + 9 / 3 - 5 + 1";
            string result = "5";
            string actual = engine.Tokenization(math);
            Assert.AreEqual(result,actual);
        }
        [TestMethod]
        public void TestMethod2()
        {
            CalculatorEngine engine = new CalculatorEngine();
            string math = "10/0";
            string result = "5";
            string actual = engine.Tokenization(math);
            Assert.AreEqual(result, actual);
        }
    }
}