using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Suncrest.ShippingCalculator.Models;

namespace Suncrest.ShippingCalculator.Tests
{
    [TestClass]
    public class ShippingCalculationTests
    {
        [TestMethod]
        public void ShippingCalculation_NoZoneForSpecifiedZip()
        {
            ShippingCalculation calc = new ShippingCalculation("55559", 2m);
            Assert.AreEqual<StatusType>(StatusType.ZoneUnknownForSpecifiedZipCode, calc.Results.Status);
        }

        [TestMethod]
        public void ShippingCalculation_InvalidZip()
        {
            ShippingCalculation calc = new ShippingCalculation("AA", 2m);
            Assert.AreEqual<StatusType>(StatusType.InvalidZipCode, calc.Results.Status);
            calc = new ShippingCalculation("5555", 2m);
            Assert.AreEqual<StatusType>(StatusType.InvalidZipCode, calc.Results.Status);
            calc = new ShippingCalculation("555555", 2m);
            Assert.AreEqual<StatusType>(StatusType.InvalidZipCode, calc.Results.Status);
        }

        [TestMethod]
        public void ShippingCalculation_WeightOutOfRange()
        {
            ShippingCalculation calc = new ShippingCalculation("55555", 0m);
            Assert.AreEqual<StatusType>(StatusType.InvalidWeight, calc.Results.Status);
            calc = new ShippingCalculation("55555", 100m);
            Assert.AreEqual<StatusType>(StatusType.InvalidWeight, calc.Results.Status);
        }

        [TestMethod]
        public void ShippingCalculation_ZoneValidButWeightMissing()
        {
            ShippingCalculation calc = new ShippingCalculation("55555", 5m);
            Assert.AreEqual<StatusType>(StatusType.CostUnknownForZipCodeAndWeight, calc.Results.Status);
            calc = new ShippingCalculation("55556", 3m);
            Assert.AreEqual<StatusType>(StatusType.CostUnknownForZipCodeAndWeight, calc.Results.Status);
            calc = new ShippingCalculation("55557", 1.5m);
            Assert.AreEqual<StatusType>(StatusType.CostUnknownForZipCodeAndWeight, calc.Results.Status);
        }

        [TestMethod]
        public void ShippingCalculation_ShouldBeValid()
        {
            ShippingCalculation calc = new ShippingCalculation("55555", 2m);
            Assert.AreEqual<StatusType>(StatusType.Success, calc.Results.Status);
            Assert.AreEqual(3.25m, calc.Results.Cost);
            calc = new ShippingCalculation("55556", 1.5m);
            Assert.AreEqual<StatusType>(StatusType.Success, calc.Results.Status);
            Assert.AreEqual(2.25m, calc.Results.Cost);
        }
    }
}
