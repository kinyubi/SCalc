using Microsoft.VisualStudio.TestTools.UnitTesting;
using Suncrest.ShippingCalculator.Models;
using System.Collections.Generic;
using System.Linq;

namespace Suncrest.ShippingCalculator.Tests
{
    [TestClass]
    public class ShippingCostsServiceClientTests
    {
        [TestMethod]
        public void ShippingCosts_GetAll_Should_Succeed()
        {
            IEnumerable<ShippingCost> rates = ShippingCostsServiceClient.GetAll();
            Assert.IsNotNull(rates);
            ShippingCost rate = rates.FirstOrDefault(r => r.Zone == "4" && r.MaxWeight == 2m);
            Assert.IsTrue(rate.Cost == 3.25m);
            Assert.IsTrue(rates.Count() == 5);
        }

        [TestMethod]
        public void ShippingCosts_GetOne_When_WeightAtHighLimit()
        {
            ShippingCost rate = ShippingCostsServiceClient.GetOne("4", 1.50m);
            Assert.IsNotNull(rate);
            Assert.IsTrue(rate.Cost == 2m);
        }

        [TestMethod]
        public void ShippingCosts_GetOne_When_WeightJustOverHighLimit()
        {
            ShippingCost rate = ShippingCostsServiceClient.GetOne("4", 2.001m);
            Assert.IsNull(rate);
        }

        [TestMethod]
        public void ShippingCosts_GetOne_When_WeightAtLowLimit()
        {
            ShippingCost rate = ShippingCostsServiceClient.GetOne("3", 1.00001m);
            Assert.IsNotNull(rate);
            Assert.IsTrue(rate.Cost == 2.25m);
        }

        [TestMethod]
        public void ShippingCosts_GetOne_When_WeightZero_ShouldBe_Null()
        {
            ShippingCost rate = ShippingCostsServiceClient.GetOne("4", 0m);
            Assert.IsNull(rate);
        }

        [TestMethod]
        public void ShippingCosts_GetOne_When_ZoneValidAndWeightOutOfRange()
        {
            ShippingCost rate = ShippingCostsServiceClient.GetOne("4", 5m);
            Assert.IsNull(rate);
        }

        [TestMethod]
        public void ShippingCosts_GetOne_When_ZoneInvalid()
        {
            ShippingCost rate = ShippingCostsServiceClient.GetOne("AAAA", 1.5m);
            Assert.IsNull(rate);
        }
    }
}
