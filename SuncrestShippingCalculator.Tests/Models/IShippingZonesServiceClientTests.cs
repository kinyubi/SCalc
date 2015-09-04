using Microsoft.VisualStudio.TestTools.UnitTesting;
using Suncrest.ShippingCalculator.Models;
using System.Collections.Generic;
using System.Linq;

namespace Suncrest.ShippingCalculator.Tests
{
    [TestClass]
    public abstract class BaseShippingZonesServiceClientTests
    {
        private IShippingZonesServiceClient _serviceClient;

        internal abstract IShippingZonesServiceClient GetServiceClient();

        [TestInitialize]
        public void ClassInitialize()
        {
            _serviceClient = GetServiceClient();
        }

        [TestMethod]
        public void ShippingZones_GetAll_Should_Succeed()
        {
            IEnumerable<ShippingZone> zones = _serviceClient.GetAll();
            Assert.IsNotNull(zones);
            ShippingZone zone = zones.FirstOrDefault(z => z.Zip == "55555");
            Assert.IsTrue(zone.Zone == "4");
        }

        [TestMethod]
        public void ShippingZones_GetOne_When_OKExpected()
        {
            ShippingZone zone = _serviceClient.GetOne("55556");
            Assert.IsNotNull(zone);
            Assert.IsTrue(zone.Zone == "3");
        }

        [TestMethod]
        public void ShippingZones_GetOne_When_NotFoundExpected()
        {
            ShippingZone zone = _serviceClient.GetOne("AAAA");
            Assert.IsNull(zone);
        }
    }

    [TestClass]
    public class ShippingZonesServiceClientTests : BaseShippingZonesServiceClientTests
    {
        internal override IShippingZonesServiceClient GetServiceClient()
        {
            return ShippingZonesServiceClient.Instance;
        }
    }

    [TestClass]
    public class FakeShippingZonesServiceClientTests : BaseShippingZonesServiceClientTests
    {
        internal override IShippingZonesServiceClient GetServiceClient()
        {
            return FakeShippingZonesServiceClient.Instance;
        }
    }
}
