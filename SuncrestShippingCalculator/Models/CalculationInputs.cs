using System.ComponentModel.DataAnnotations;

//TODO: This is a subset of the CalculationResults object. Refactor solution to use CalculationResult object

namespace Suncrest.ShippingCalculator.Models
{
    /// <summary>
    /// Represents the inputs provided by the user for the calculation
    /// </summary>
    public class CalculationInputs : ICalculationInputs
    {
        /// <summary>
        /// Gets or sets the zip code input by the user.
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the weight input by the user.
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationInputs"/> class.
        /// </summary>
        public CalculationInputs()
        {
            ZipCode = "Unspecified";
            Weight = 0m;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationInputs"/> class.
        /// </summary>
        /// <param name="zipCode">The zip code.</param>
        /// <param name="weight">The weight.</param>
        public CalculationInputs(string zipCode, decimal weight)
        {
            ZipCode = zipCode;
            Weight = weight;
        }
    }
}