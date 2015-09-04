using System.ComponentModel.DataAnnotations;

namespace Suncrest.ShippingCalculator.Models
{
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

    public class CalculationResults
    {
        [Required(ErrorMessage = "The ZipCode cannot be blank")]
        [Display(Name = "ZipCode:")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "ZipCode must be a 5-digit number. (You're not in Canada, eh!)")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Weight cannot be blank")]
        [Display(Name = "Weight(lbs):")]
        [Range(0.01, 99.99, ErrorMessage = "Weight must be > 0 and < 100")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N}")]
        public decimal Weight { get; set; }

        public string Zone { get; set; }

        [DisplayFormat(DataFormatString="{0:C}")]
        public decimal Cost { get; set; }

        public StatusType Status { get; set; }
        public string ErrorMessage { get; set; }

        public CalculationResults() 
        {
            Zone = null;
            ZipCode = null;
            Weight = 0m;
            Cost = 0m;
            Status = StatusType.Unknown;
            ErrorMessage = "";
        }

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