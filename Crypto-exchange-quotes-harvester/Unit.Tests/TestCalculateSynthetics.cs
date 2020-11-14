using System;
using Core.Interfaces;
using Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unit.Tests
{
    [TestClass]
    public class TestCalculateSynthetics
    {
        private ICalculateSyntheticQuotes calculator;

        public TestCalculateSynthetics()
        {
            calculator = new CalculateSyntheticQuotes();
        }
        [TestMethod]
        public void TestMethod1()
        {
            var result = calculator.Calculate(7698, 0.84M);
            Assert.IsTrue(Math.Ceiling(result) == 9165);
        }
        [TestMethod]
        public void TestMethod2()
        {
            var result = calculator.Calculate(7698, 0.84M);
            Assert.IsFalse(Math.Ceiling(result) == 10.00M);
        }
    }
}
