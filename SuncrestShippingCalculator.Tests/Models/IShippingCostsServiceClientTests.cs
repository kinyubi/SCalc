using Microsoft.VisualStudio.TestTools.UnitTesting;
using Suncrest.ShippingCalculator.Models;
using System.Collections.Generic;
using System.Linq;

namespace Suncrest.ShippingCalculator.Tests
{

    [TestClass]
    public abstract class BaseShippingCostsServiceClientTests
    {
        private IShippingCostsServiceClient _serviceClient;

        //Use the constructor to set _serviceClient to the implementation
        internal abstract IShippingCostsServiceClient GetServiceClient();

        [TestInitialize]
        public void ClassInitialize()
        {
            _serviceClient = GetServiceClient();
        }

        [TestMethod]
        public void ShippingCosts_GetAll_Should_Succeed()
        {
            IEnumerable<ShippingCost> rates = _serviceClient.GetAll();
            Assert.IsNotNull(rates);
            ShippingCost rate = rates.FirstOrDefault(r => r.Zone == "4" && r.MaxWeight == 2m);
            Assert.IsTrue(rate.Cost == 3.25m);
            Assert.IsTrue(rates.Count() == 5);
        }

        [TestMethod]
        public void ShippingCosts_GetOne_When_WeightAtHighLimit()
        {
            ShippingCost rate = _serviceClient.GetOne("4", 1.50m);
            Assert.IsNotNull(rate);
            Assert.IsTrue(rate.Cost == 2m);
        }

        [TestMethod]
        public void ShippingCosts_GetOne_When_WeightJustOverHighLimit()
        {
            ShippingCost rate = _serviceClient.GetOne("4", 2.001m);
            Assert.IsNull(rate);
        }

        [TestMethod]
        public void ShippingCosts_GetOne_When_WeightAtLowLimit()
        {
            ShippingCost rate = _serviceClient.GetOne("3", 1.00001m);
            Assert.IsNotNull(rate);
            Assert.IsTrue(rate.Cost == 2.25m);
        }

        [TestMethod]
        public void ShippingCosts_GetOne_When_WeightZero_ShouldBe_Null()
        {
            ShippingCost rate = _serviceClient.GetOne("4", 0m);
            Assert.IsNull(rate);
        }

        [TestMethod]
        public void ShippingCosts_GetOne_When_ZoneValidAndWeightOutOfRange()
        {
            ShippingCost rate = _serviceClient.GetOne("4", 5m);
            Assert.IsNull(rate);
        }

        [TestMethod]
        public void ShippingCosts_GetOne_When_ZoneInvalid()
        {
            ShippingCost rate = _serviceClient.GetOne("AAAA", 1.5m);
            Assert.IsNull(rate);
        }
    }

    [TestClass]
    public class ShippingCostsServiceClientTests : BaseShippingCostsServiceClientTests
    {
        internal override IShippingCostsServiceClient GetServiceClient()
        {
            return ShippingCostsServiceClient.Instance;
        }
    }

    [TestClass]
    public class FakeShippingCostsServiceClientTests : BaseShippingCostsServiceClientTests
    {
        internal override IShippingCostsServiceClient GetServiceClient()
        {
            return FakeShippingCostServiceClient.Instance;
        }
    }
}
