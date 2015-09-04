using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suncrest.ShippingCalculator.Models
{
    /// <summary>
    /// Required interface for a ShippingZonesServiceClient includes a GetOne and GetAll method
    /// </summary>
    public interface IShippingZonesServiceClient
    {
        /// <summary>
        /// Gets the ShippingZone object associated with the zip parameter.
        /// </summary>
        /// <param name="zip">a zipcode.</param>
        /// <returns>should return a ShippingZone object or null if none found</returns>
        ShippingZone GetOne(string zip);

        /// <summary>
        /// Gets the entire array of ShippingZone objects.
        /// </summary>
        /// <returns>an entire array of ShippingZone objects</returns>
        IEnumerable<ShippingZone> GetAll();
    }
}
