using System.ComponentModel.DataAnnotations;

namespace Suncrest.ShippingCalculator.Models
{
    public class CalculationInputs
    {
        [Required(ErrorMessage = "The ZipCode cannot be blank")]
        [Display(Name = "ZipCode:")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "ZipCode must be a 5-digit number. (You're not in Canada, eh!)")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Weight cannot be blank")]
        [Display(Name = "Weight(lbs):")]
        [Range(0.01, 99.99, ErrorMessage = "Weight must be > 0 and < 100")]
        [DataType(DataType.Currency)]
        public decimal Weight { get; set; }

        public CalculationInputs()
        {
            ZipCode = "Unspecified";
            Weight = 0m;
        }
        public CalculationInputs(string zipCode, decimal weight)
        {
            ZipCode = zipCode;
            Weight = weight;
        }
    }
}