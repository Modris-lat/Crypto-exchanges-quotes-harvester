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
            
        }
    }
}
