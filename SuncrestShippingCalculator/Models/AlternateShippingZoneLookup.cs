using System;
using System.Collections.Generic;
using System.Linq;
using Suncrest.ShippingCalculator.Models;

namespace Suncrest.ShippingCalculator.Models
{
    /// <summary>
    /// Implementation of the IShippingZonesServiceClient interface. This uses
    /// hard-coded values rather that querying a service.
    /// </summary>
    public class AlternateShippingZonesLookup : IShippingZonesLookup
    {
        /// <summary>
        /// A table of ShippingZone objects representing zipcode-to-zone relationships
        /// </summary>
        private ShippingZoneLookupEntry[] _zones;

        /// <summary>
        /// Prevents a default instance of the <see cref="AlternateShippingZonesLookup"/> class from being created.
        /// </summary>
        public AlternateShippingZonesLookup()
        {
            _zones = new ShippingZoneLookupEntry[]
            {
                new ShippingZoneLookupEntry("55555","4"),
                new ShippingZoneLookupEntry("55556","3"),
                new ShippingZoneLookupEntry("55557","9")
            };
        }

        /// <summary>
        /// Gets the ShippingZone object associated with the zip parameter.
        /// </summary>
        /// <param name="zip">a zipcode.</param>
        /// <returns>ShippingZone object or null if none found</returns>
        public IShippingZoneLookupEntry GetOne(string zip)
        {
            IShippingZoneLookupEntry zipZone;
            try
            {
                zipZone = _zones.First((z) => z.Zip == zip);
            }
            catch (InvalidOperationException)
            {
                zipZone = null;
            }
            return zipZone;
        }

        /// <summary>
        /// Gets the entire array of ShippingZone objects.
        /// </summary>
        /// <returns>an entire array of Shippingzone objects</returns>
        public IEnumerable<IShippingZoneLookupEntry> GetAll()
        {
            return _zones;
        }
    }
}