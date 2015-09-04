using System.ComponentModel.DataAnnotations;

namespace Suncrest.ShippingCalculator.Models
{

    /// <summary>
    /// StatusType defines enumerations for the Status property in a 
    /// CalculationResults object. The ShippingCalculation class sets
    /// the Status value as it builds the the CalculationResults object.
    /// </summary>
    public enum StatusType
    {
        Success,
        InvalidZipCode,
        InvalidWeight,
        ZoneUnknownForSpecifiedZipCode,
        CostUnknownForZipCodeAndWeight,
        UnknownError,
        Unknown
    };

    /// <summary>
    /// Created when the user provides a zip code and weight in the Index action in either
    /// the Home controller or Ajax controller. 
    /// </summary>
    public class CalculationResults
    {
        /// <summary>
        /// Gets or sets the zip code. This is an input to the shipping cost calculation.
        /// The associated attributes are for input validation.
        /// </summary>
        [Required(ErrorMessage = "The ZipCode cannot be blank")]
        [Display(Name = "ZipCode:")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "ZipCode must be a 5-digit number. (You're not in Canada, eh!)")]
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the weight. This is an input to the shipping clost calculation
        /// The associated attributes are for input validation.
        /// </summary>
        [Required(ErrorMessage = "Weight cannot be blank")]
        [Display(Name = "Weight(lbs):")]
        [Range(0.00001, 99.99, ErrorMessage = "Weight must be > 0 and < 100")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N}")]
        public decimal Weight { get; set; }

        /// <summary>
        /// Gets or sets the zone. Each zip code is associated with a zone and is retrieved
        /// by an implementation of the IShippingZonesServiceClient interface. Currently this 
        /// is a single-digit string.
        /// </summary>
        public string Zone { get; set; }

        /// <summary>
        /// Gets or sets the cost. This value is retrieved by an implementation of the 
        /// IShippingCostsServiceClient interface based on a zone and and weight.
        /// </summary>
        [DisplayFormat(DataFormatString="{0:C}")]
        public decimal Cost { get; set; }

        /// <summary>
        /// Gets or sets the completion status of the calculation. The enumerated value indicates
        /// success or the reason for the calculation's failure.
        /// </summary>
        public StatusType Status { get; set; }

        /// <summary>
        /// Gets or sets the error message. This is a displayable message reflecting the 
        /// completion status of the calculation. Blank is calculation is a success
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationResults"/> class.
        /// </summary>
        public CalculationResults() : this(null, 0m) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationResults"/> class.
        /// </summary>
        /// <param name="zip">The zip code input by the user.</param>
        /// <param name="weight">The weight input by the user.</param>
        public CalculationResults(string zip, decimal weight)
        {
            Zone = null;
            ZipCode= zip;
            Weight = weight;
            Cost = 0m;
            Status = StatusType.Unknown;
            ErrorMessage = "";
        }
    }
}