using Microsoft.VisualStudio.TestTools.UnitTesting;
using Suncrest.ShippingCalculator.Models;
using System.Collections.Generic;
using System.Linq;

namespace Suncrest.ShippingCalculator.Tests
{
    [TestClass]
    public class ShippingZonesServiceClientTests
    {
        [TestMethod]
        public void ShippingZones_GetAll_Should_Succeed()
        {
            IEnumerable<ShippingZone> zones = ShippingZonesServiceClient.GetAll();
            Assert.IsNotNull(zones);
            ShippingZone zone = zones.FirstOrDefault(z => z.Zip == "55555");
            Assert.IsTrue(zone.Zone == "4");
        }

        [TestMethod]
        public void ShippingZones_GetOne_When_OKExpected()
        {
            ShippingZone zone = ShippingZonesServiceClient.GetOne("55556");
            Assert.IsNotNull(zone);
            Assert.IsTrue(zone.Zone == "3");
        }

        [TestMethod]
        public void ShippingZones_GetOne_When_NotFoundExpected()
        {
            ShippingZone zone = ShippingZonesServiceClient.GetOne("AAAA");
            Assert.IsNull(zone);
        }
    }
}
