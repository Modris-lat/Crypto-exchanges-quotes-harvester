using System;
using Core.Interfaces;
using Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unit.Tests
{
    [TestClass]
    public class InputSettingsTest
    {
        private ISettingsConfig settings;

        public InputSettingsTest()
        {
            settings = new DataBaseSettings();
        }
        [TestMethod]
        public void TestChooseSettings()
        {
            settings.ChooseSettings();
        }
    }
}
