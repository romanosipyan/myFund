using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using myFund.Converters;
using System.Windows.Media;

namespace myFund.Tests.UnitTests
{
    [TestClass]
    public class ColorConverterTest
    {
        [TestMethod]
        public void CanInitialize()
        {
            try
            {
                var converter = new Converters.ColorConverter();
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Convert()
        {
            var converter = new Converters.ColorConverter();
            var isNegative = false;
            var result = converter.Convert(isNegative, null, null, null);

            Assert.IsTrue(result.Equals(Brushes.White));
        }

        [TestMethod]
        public void ConvertBack()
        {
            var converter = new Converters.ColorConverter();
            try
            {
                var result = converter.ConvertBack(null, null, null, null);
                Assert.Fail("Converter should be only one way");
            }
            catch(Exception e)
            {
                Assert.IsTrue(e is NotImplementedException);
            }           
        }
    }
}
