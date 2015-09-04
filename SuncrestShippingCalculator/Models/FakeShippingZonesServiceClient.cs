using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Suncrest.ShippingCalculator.Models
{
    public class FakeShippingZonesServiceClient : IShippingZonesServiceClient
    {
        private ShippingZone[] _zones;

        private static readonly Lazy<FakeShippingZonesServiceClient> lazy = new Lazy<FakeShippingZonesServiceClient>(() => new FakeShippingZonesServiceClient());
        public static FakeShippingZonesServiceClient Instance { get { return lazy.Value; } }

        private FakeShippingZonesServiceClient()
        {
            _zones = new ShippingZone[]
            {
                new ShippingZone("55555","4"),
                new ShippingZone("55556","3"),
                new ShippingZone("55557","9")
            };
        }

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

        public IEnumerable<ShippingZone> GetAll()
        {
            return _zones;
        }
    }
}