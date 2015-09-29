using System.Collections.Generic;

namespace Suncrest.ShippingCalculator.Models
{
    /// <summary>
    /// Required interface for a ShippingCostServiceClient includes a GetOne and GetAll method
    /// </summary>
    public interface IShippingCostsLookup
    {
        IShippingCostLookupEntry GetOne(string zone, decimal weight);
        IEnumerable<IShippingCostLookupEntry> GetAll();
    }
}
