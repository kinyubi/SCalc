using System.ComponentModel.DataAnnotations;

namespace Suncrest.ShippingCalculator.Models
{
    /// <summary>
    /// Represents an entry in the ShippingCosts lookup table. It consists of a
    /// Zone, MinWeight, MaxWeight and the associated Cost.
    /// The lookup algorithm matches is the given weight is greater than the MinWeight and
    /// less than or equal to the MaxWeight.
    /// </summary>
    public class ShippingCostLookupEntry : IShippingCostLookupEntry
    {
        /// <summary>
        /// Gets or sets the zone for this table entry.
        /// </summary>
        [Display(Name = "Zone:")]
        public string Zone { get; set; }

        /// <summary>
        /// Gets or sets the lower bound of the weight range. To match with this entry a given
        /// weight must be greater tan MinWeight and less than or equal to MaxWeight
        /// </summary>
        public decimal MinWeight { get; set; }

        /// <summary>
        /// Gets or sets the upper bound of the weight range. To match with this entry a given
        /// weight must be greater tan MinWeight and less than or equal to MaxWeight
        /// </summary>
        public decimal MaxWeight { get; set; }

        /// <summary>
        /// Gets or sets the cost.
        /// </summary>
        [Display(Name = "Cost:")]
        [DataType(DataType.Currency)]
        public decimal Cost { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShippingCostLookupEntry"/> class.
        /// </summary>
        /// <param name="zone">The zone.</param>
        /// <param name="minWeight">The minimum weight. Given weight must be greater than</param>
        /// <param name="maxWeight">The maximum weight. Given weight must be less than or equal</param>
        /// <param name="cost">The cost associated with a zone and weight range.</param>
        public ShippingCostLookupEntry(string zone, decimal minWeight, decimal maxWeight, decimal cost)
        {
            Zone = zone;
            MinWeight = minWeight;
            MaxWeight = maxWeight;
            Cost = cost;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShippingCostLookupEntry"/> class.
        /// </summary>
        public ShippingCostLookupEntry() : this(null, 0m, 0m, 0m) { }

        
    }

    
}