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
            string math = "1+2";
            string result = "3";
            string actual = engine.Evaluate(math).ToString();
            Assert.AreEqual(result,actual);
        }
        [TestMethod]
        public void TestMethod2()
        {
            CalculatorEngine engine = new CalculatorEngine();
            string math = "2-1";
            string result = "1";
            string actual = engine.Evaluate(math).ToString();
            Assert.AreEqual(result, actual);
        }
    }
}