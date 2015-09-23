using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Suncrest.ShippingCalculator.Models
{
    /// <summary>
    /// Implementation of the IShippingCostsServiceClient interface. This uses
    /// hard-coded values rather that querying a service.
    /// </summary>
    public class FakeShippingCostServiceClient : IShippingCostsServiceClient
    {
        /// <summary>
        /// The _rates array is populated by the constructor.
        /// </summary>
        private ShippingCost[] _rates;

        /// <summary>
        /// Used as part of the singleton pattern
        /// </summary>
        private static readonly Lazy<FakeShippingCostServiceClient> lazy = new Lazy<FakeShippingCostServiceClient>(() => new FakeShippingCostServiceClient());

        /// <summary>
        /// Gets the singleton instance of this class.
        /// </summary>
        public static FakeShippingCostServiceClient Instance {get {return lazy.Value;}}

        /// <summary>
        /// Prevents a default instance of the <see cref="FakeShippingCostServiceClient"/> class from being created.
        /// </summary>
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

        /// <summary>
        /// Gets the ShippingCost object based on zone and weight.
        /// </summary>
        /// <param name="zone">The zone.</param>
        /// <param name="weight">The weight.</param>
        /// <returns>The associated ShippingCost object or null if not found</returns>
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

        /// <summary>
        /// Gets the entire array of ShippingCost objects.
        /// </summary>
        /// <returns>The entire table of ShippingCost objects</returns>
        public IEnumerable<ShippingCost> GetAll()
        {
            return _rates;
        }

        public string ServiceUri { get { return "fake"; } }
    }
}