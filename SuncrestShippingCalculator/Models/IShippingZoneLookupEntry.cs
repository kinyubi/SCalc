using System;
namespace Suncrest.ShippingCalculator.Models
{
    public interface IShippingZoneLookupEntry
    {
        string Zip { get; set; }
        string Zone { get; set; }
    }
}
