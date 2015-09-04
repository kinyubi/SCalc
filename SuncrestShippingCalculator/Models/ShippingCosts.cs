using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Suncrest.ShippingCalculator.Models
{
    public class ShippingCosts
    {
        public static IEnumerable<ShippingCost> List { get; set; }

        static ShippingCosts()
        {
            IShippingCostsServiceClient costClient = ServiceFactory.GetCostsClient();
            List = costClient.GetAll();
        }
    }
}