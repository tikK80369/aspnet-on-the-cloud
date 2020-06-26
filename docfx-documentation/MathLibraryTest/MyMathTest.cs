#pragma warning disable 0436
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathLibrary;

namespace MathLibraryTest
{
    [TestClass]
    public class MyMathTest
    {
        [TestMethod]
        public void TestAddInt()
        {
            int number1 = 2;
            int number2 = 3;
            int expected = 5;
            int result = MyMath.Add(number1, number2);
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void TestAddDouble()
        {
            double number1 = 2;
            double number2 = 3;
            double expected = 5;
            double result = MyMath.Add(number1, number2);
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        [ExpectedException(typeof(System.OverflowException))]
        public void TestAddIntOverflow()
        {
            int number1 = 2147483647;
            int number2 = 3;
            MyMath.Add(number1, number2);
        }
        [TestMethod]
        [ExpectedException(typeof(System.OverflowException))]
        public void TestAddDoubleOverflow()
        {
            double number1 = 1.7976931348623157E+308;
            double number2 = 3;
            MyMath.Add(number1, number2);
        }
        [TestMethod]
        public void TestSubtractInt()
        {
            int number1 = 7;
            int number2 = 3;
            int expected = 4;
            int result = MyMath.Subtract(number1, number2);
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void TestSubtractDouble()
        {
            double number1 = 7;
            double number2 = 3;
            double expected = 4;
            double result = MyMath.Subtract(number1, number2);
            Assert.AreEqual(result, expected);
        }
        [TestMethod]
        public void TestMultiplyInt()
        {
            int number1 = 7;
            int number2 = 3;
            int expected = 21;
            int result = MyMath.Multiply(number1, number2);
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void TestMultiplyDouble()
        {
            double number1 = 7;
            double number2 = 3;
            double expected = 21;
            double result = MyMath.Multiply(number1, number2);
            Assert.AreEqual(result, expected);
        }
        [TestMethod]
        public void TestDivideInt()
        {
            int number1 = 9;
            int number2 = 3;
            int expected = 3;
            int result = MyMath.Divide(number1, number2);
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void TestDivideDouble()
        {
            double number1 = 9;
            double number2 = 3;
            double expected = 3;
            double result = MyMath.Divide(number1, number2);
            Assert.AreEqual(result, expected);
        }
    }
}