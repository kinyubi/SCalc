using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Suncrest.ShippingCalculator.Models
{
    public class FakeShippingCostServiceClient : IShippingCostsServiceClient
    {
        private ShippingCost[] _rates;

        private static readonly Lazy<FakeShippingCostServiceClient> lazy = new Lazy<FakeShippingCostServiceClient>(() => new FakeShippingCostServiceClient());
        public static FakeShippingCostServiceClient Instance {get {return lazy.Value;}}

        private FakeShippingCostServiceClient()
        {
            _rates = new ShippingCost[] 
            {
                new ShippingCost("4", 0.00m, 1.00m, 1.25m),
                new ShippingCost("4", 1.00m, 1.50m, 2.00m),
                new ShippingCost("4", 1.50m, 2.00m, 3.25m),
                new ShippingCost("3", 0m, 1m, 1.75m),
                new ShippingCost("3", 1.00m, 1.50m, 2.25m)
            };
        }
        public ShippingCost GetOne(string zone, decimal weight)
        {
            ShippingCost rate;
            try
            {
                rate = _rates.First(r => ((r.Zone == zone) && (weight > r.MinWeight) && (weight <= r.MaxWeight)));
            }
            catch (InvalidOperationException)
            {
                rate = null;
            }

            return rate;
        }

        public IEnumerable<ShippingCost> GetAll()
        {
            return _rates;
        }
    }
}