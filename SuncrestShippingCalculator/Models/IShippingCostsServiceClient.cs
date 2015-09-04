using System.Collections.Generic;

namespace Suncrest.ShippingCalculator.Models
{
    public interface IShippingCostsServiceClient
    {
        ShippingCost GetOne(string zone, decimal weight);
        IEnumerable<ShippingCost> GetAll();
    }
}
