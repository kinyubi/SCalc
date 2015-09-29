using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Suncrest.ShippingCalculator.Models;

namespace Suncrest.ShippingCalculator.Tests
{
    [TestClass]
    public class ShippingCalculationTests
    {
        Calculator calc;

        public ShippingCalculationTests()
        {
            IShippingCostsLookup costsLookup = new AlternateShippingCostsLookup();
            IShippingZonesLookup zonesLookup = new AlternateShippingZonesLookup();
            calc = new Calculator(zonesLookup, costsLookup);
        }

        [TestMethod]
        public void ShippingCalculation_NoZoneForSpecifiedZip()
        {
            var results = calc.Compute("55559", 2m);
            Assert.AreEqual<StatusType>(StatusType.ZoneUnknownForSpecifiedZipCode, results.Status);
        }

        [TestMethod]
        public void ShippingCalculation_InvalidZip()
        {
            var results = calc.Compute("AA", 2m);
            Assert.AreEqual<StatusType>(StatusType.InvalidZipCode, results.Status);
            results = calc.Compute("5555", 2m);
            Assert.AreEqual<StatusType>(StatusType.InvalidZipCode, results.Status);
            results = calc.Compute("555555", 2m);
            Assert.AreEqual<StatusType>(StatusType.InvalidZipCode, results.Status);
        }

        [TestMethod]
        public void ShippingCalculation_WeightOutOfRange()
        {
            var results = calc.Compute("55555", 0m);
            Assert.AreEqual<StatusType>(StatusType.InvalidWeight, results.Status);
            results = calc.Compute("55555", 100m);
            Assert.AreEqual<StatusType>(StatusType.InvalidWeight, results.Status);
        }

        [TestMethod]
        public void ShippingCalculation_ZoneValidButWeightMissing()
        {
            var results = calc.Compute("55555", 5m);
            Assert.AreEqual<StatusType>(StatusType.CostUnknownForZipCodeAndWeight, results.Status);
            results = calc.Compute("55556", 3m);
            Assert.AreEqual<StatusType>(StatusType.CostUnknownForZipCodeAndWeight, results.Status);
            results = calc.Compute("55557", 1.5m);
            Assert.AreEqual<StatusType>(StatusType.CostUnknownForZipCodeAndWeight, results.Status);
        }

        [TestMethod]
        public void ShippingCalculation_ShouldBeValid()
        {
            var results = calc.Compute("55555", 2m);
            Assert.AreEqual<StatusType>(StatusType.Success, calc.Results.Status);
            Assert.AreEqual(3.25m, calc.Results.Cost);
            results = calc.Compute("55556", 1.5m);
            Assert.AreEqual<StatusType>(StatusType.Success, calc.Results.Status);
            Assert.AreEqual(2.25m, calc.Results.Cost);
        }
    }
}
