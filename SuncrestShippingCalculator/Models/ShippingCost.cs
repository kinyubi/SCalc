using System.ComponentModel.DataAnnotations;

namespace Suncrest.ShippingCalculator.Models
{
    public class ShippingCost
    {
        [Display(Name = "Zone:")]
        public string Zone { get; set; }

        public decimal MinWeight { get; set; }
        public decimal MaxWeight { get; set; }

        [Display(Name = "Cost:")]
        [DataType(DataType.Currency)]
        public decimal Cost { get; set; }

        public ShippingCost(string zone, decimal minWeight, decimal maxWeight, decimal cost)
        {
            Zone = zone;
            MinWeight = minWeight;
            MaxWeight = maxWeight;
            Cost = cost;
        }

        public ShippingCost() : this(null, 0m, 0m, 0m) { }

        
    }

    
}