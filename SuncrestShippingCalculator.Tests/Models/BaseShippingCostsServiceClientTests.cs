using Microsoft.VisualStudio.TestTools.UnitTesting;
using Suncrest.ShippingCalculator.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using Microsoft.Practices.Unity;

namespace Suncrest.ShippingCalculator.Tests
{

    [TestClass]
    public abstract class BaseShippingCostsServiceClientTests
    {
        protected IShippingCostsLookup _serviceClient;

        [TestMethod]
        public void ShippingCosts_GetAll_Should_Succeed()
        {
            IEnumerable<IShippingCostLookupEntry> rates = _serviceClient.GetAll();
            Assert.IsNotNull(rates);
            IShippingCostLookupEntry rate = rates.FirstOrDefault(r => r.Zone == "4" && r.MaxWeight == 2m);
            Assert.IsTrue(rate.Cost == 3.25m);
            Assert.IsTrue(rates.Count() == 5);
        }

        [TestMethod]
        public void ShippingCosts_GetOne_When_WeightAtHighLimit()
        {
            IShippingCostLookupEntry rate = _serviceClient.GetOne("4", 1.50m);
            Assert.IsNotNull(rate);
            Assert.IsTrue(rate.Cost == 2m);
        }

        [TestMethod]
        public void ShippingCosts_GetOne_When_WeightJustOverHighLimit()
        {
            IShippingCostLookupEntry rate = _serviceClient.GetOne("4", 2.001m);
            Assert.IsNull(rate);
        }

        [TestMethod]
        public void ShippingCosts_GetOne_When_WeightAtLowLimit()
        {
            IShippingCostLookupEntry rate = _serviceClient.GetOne("3", 1.00001m);
            Assert.IsNotNull(rate);
            Assert.IsTrue(rate.Cost == 2.25m);
        }

        [TestMethod]
        public void ShippingCosts_GetOne_When_WeightZero_ShouldBe_Null()
        {
            IShippingCostLookupEntry rate = _serviceClient.GetOne("4", 0m);
            Assert.IsNull(rate);
        }

        [TestMethod]
        public void ShippingCosts_GetOne_When_ZoneValidAndWeightOutOfRange()
        {
            IShippingCostLookupEntry rate = _serviceClient.GetOne("4", 5m);
            Assert.IsNull(rate);
        }

        [TestMethod]
        public void ShippingCosts_GetOne_When_ZoneInvalid()
        {
            IShippingCostLookupEntry rate = _serviceClient.GetOne("AAAA", 1.5m);
            Assert.IsNull(rate);
        }

        [TestMethod]
        public void ShippingCosts_DefaultConstructor()
        {
            CalculationInputs inputs = new CalculationInputs();
            Assert.IsTrue(inputs.Weight == 0m);
            Assert.IsTrue(inputs.ZipCode == "Unspecified");
        }
    }

    [TestClass]
    public class ShippingCostsServiceClientTests : BaseShippingCostsServiceClientTests
    {
        public ShippingCostsServiceClientTests()
        {
            _serviceClient = new DefaultShippingCostLookup();
        }
    }

    [TestClass]
    public class AlternateShippingCostsServiceClientTests : BaseShippingCostsServiceClientTests
    {
        public AlternateShippingCostsServiceClientTests()
        {
            _serviceClient = new AlternateShippingCostsLookup();
        }
    }
}
