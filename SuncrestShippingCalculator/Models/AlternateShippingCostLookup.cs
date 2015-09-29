using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Suncrest.ShippingCalculator.Models;

namespace Suncrest.ShippingCalculator.Models
{
    /// <summary>
    /// Implementation of the IShippingCostsServiceClient interface. This uses
    /// hard-coded values rather that querying a service.
    /// </summary>
    public class AlternateShippingCostsLookup : IShippingCostsLookup
    {
        private ShippingCostLookupEntry[] _rates;

        /// <summary>
        /// Prevents a default instance of the <see cref="AlternateShippingCostsLookup"/> class from being created.
        /// </summary>
        public AlternateShippingCostsLookup()
        {
            _rates = new ShippingCostLookupEntry[] 
            {
                new ShippingCostLookupEntry("4", 0.00m, 1.00m, 1.25m),
                new ShippingCostLookupEntry("4", 1.00m, 1.50m, 2.00m),
                new ShippingCostLookupEntry("4", 1.50m, 2.00m, 3.25m),
                new ShippingCostLookupEntry("3", 0m, 1m, 1.75m),
                new ShippingCostLookupEntry("3", 1.00m, 1.50m, 2.25m)
            };
        }

        /// <summary>
        /// Gets the ShippingCost object based on zone and weight.
        /// </summary>
        /// <param name="zone">The zone.</param>
        /// <param name="weight">The weight.</param>
        /// <returns>The associated ShippingCost object or null if not found</returns>
        public IShippingCostLookupEntry GetOne(string zone, decimal weight)
        {
            IShippingCostLookupEntry rate;
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

        /// <summary>
        /// Gets the entire array of ShippingCost objects.
        /// </summary>
        /// <returns>The entire table of ShippingCost objects</returns>
        public IEnumerable<IShippingCostLookupEntry> GetAll()
        {
            return _rates;
        }
    }
}