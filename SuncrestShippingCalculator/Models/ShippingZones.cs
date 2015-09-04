using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Suncrest.ShippingCalculator.Models
{
    public class ShippingZones
    {
        public static IEnumerable<ShippingZone> List { get; set; }

        static ShippingZones()
        {
            IShippingZonesServiceClient zoneClient = ServiceFactory.GetZonesClient();
            List = zoneClient.GetAll();
        }
    }
}