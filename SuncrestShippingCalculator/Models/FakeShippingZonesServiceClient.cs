using System;
using System.Collections.Generic;
using System.Linq;

namespace Suncrest.ShippingCalculator.Models
{
    /// <summary>
    /// Implementation of the IShippingZonesServiceClient interface. This uses
    /// hard-coded values rather that querying a service.
    /// </summary>
    public class FakeShippingZonesServiceClient : IShippingZonesServiceClient
    {
        /// <summary>
        /// A table of ShippingZone objects representing zipcode-to-zone relationships
        /// </summary>
        private ShippingZone[] _zones;

        /// <summary>
        /// This is a singleton class implementation object
        /// </summary>
        private static readonly Lazy<FakeShippingZonesServiceClient> lazy = new Lazy<FakeShippingZonesServiceClient>(() => new FakeShippingZonesServiceClient());

        /// <summary>
        /// Gets the singleton instance of this class.
        /// </summary>
        public static FakeShippingZonesServiceClient Instance { get { return lazy.Value; } }

        /// <summary>
        /// Prevents a default instance of the <see cref="FakeShippingZonesServiceClient"/> class from being created.
        /// </summary>
        private FakeShippingZonesServiceClient()
        {
            _zones = new ShippingZone[]
            {
                new ShippingZone("55555","4"),
                new ShippingZone("55556","3"),
                new ShippingZone("55557","9")
            };
        }

        /// <summary>
        /// Gets the ShippingZone object associated with the zip parameter.
        /// </summary>
        /// <param name="zip">a zipcode.</param>
        /// <returns>ShippingZone object or null if none found</returns>
        public ShippingZone GetOne(string zip)
        {
            ShippingZone zipZone;
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
        public IEnumerable<ShippingZone> GetAll()
        {
            return _zones;
        }
    }
}