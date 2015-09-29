using System.Collections.Generic;

namespace Suncrest.ShippingCalculator.Models
{
    /// <summary>
    /// Required interface for a ShippingZonesServiceClient includes a GetOne and GetAll method
    /// </summary>
    public interface IShippingZonesLookup
    {
        IShippingZoneLookupEntry GetOne(string zip);
        IEnumerable<IShippingZoneLookupEntry> GetAll();
    }
}
