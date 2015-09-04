using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suncrest.ShippingCalculator.Models
{
    public interface IShippingZonesServiceClient
    {
        ShippingZone GetOne(string zip);
        IEnumerable<ShippingZone> GetAll();
    }
}
