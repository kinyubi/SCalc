using Microsoft.VisualStudio.TestTools.UnitTesting;
using Suncrest.ShippingCalculator.Models;
using System.Collections.Generic;
using System.Linq;

namespace Suncrest.ShippingCalculator.Tests
{
    [TestClass]
    public abstract class BaseShippingZonesServiceClientTests
    {
        protected IShippingZonesLookup _serviceClient;

        [TestMethod]
        public void ShippingZones_GetAll_Should_Succeed()
        {
            IEnumerable<IShippingZoneLookupEntry> zones = _serviceClient.GetAll();
            Assert.IsNotNull(zones);
            IShippingZoneLookupEntry zone = zones.FirstOrDefault(z => z.Zip == "55555");
            Assert.IsTrue(zone.Zone == "4");
        }

        [TestMethod]
        public void ShippingZones_GetOne_When_OKExpected()
        {
            IShippingZoneLookupEntry zone = _serviceClient.GetOne("55556");
            Assert.IsNotNull(zone);
            Assert.IsTrue(zone.Zone == "3");
        }

        [TestMethod]
        public void ShippingZones_GetOne_When_NotFoundExpected()
        {
            IShippingZoneLookupEntry zone = _serviceClient.GetOne("AAAA");
            Assert.IsNull(zone);
        }
    }

    [TestClass]
    public class DefaultShippingZonesServiceClientTests : BaseShippingZonesServiceClientTests
    {
        public DefaultShippingZonesServiceClientTests()
        {
            _serviceClient =  new DefaultShippingZoneLookup();
        }
    }

    [TestClass]
    public class AlternateShippingZonesServiceClientTests : BaseShippingZonesServiceClientTests
    {
        public AlternateShippingZonesServiceClientTests()
        {
            _serviceClient =  new DefaultShippingZoneLookup();
        }
    }
}
