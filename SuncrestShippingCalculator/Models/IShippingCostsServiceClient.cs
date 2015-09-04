using System.Collections.Generic;

namespace Suncrest.ShippingCalculator.Models
{
    /// <summary>
    /// Required interface for a ShippingCostServiceClient includes a GetOne and GetAll method
    /// </summary>
    public interface IShippingCostsServiceClient
    {
        /// <summary>
        /// Gets the ShippingCost object based on zone and weight.
        /// </summary>
        /// <param name="zone">The zone.</param>
        /// <param name="weight">The weight.</param>
        /// <returns>The associated ShippingCost object or null if not found</returns>
        ShippingCost GetOne(string zone, decimal weight);

        /// <summary>
        /// Gets the entire array of ShippingCost objects.
        /// </summary>
        /// <returns>The entire table of ShippingCost objects</returns>
        IEnumerable<ShippingCost> GetAll();
    }
}
