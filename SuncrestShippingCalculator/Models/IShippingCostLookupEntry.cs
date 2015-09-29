using System;
namespace Suncrest.ShippingCalculator.Models
{
    public interface IShippingCostLookupEntry
    {
        decimal Cost { get; set; }
        decimal MaxWeight { get; set; }
        decimal MinWeight { get; set; }
        string Zone { get; set; }
    }
}
