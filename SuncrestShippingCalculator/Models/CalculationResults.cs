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
        public string Zip { get; set; }
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
            Zip = null;
            Weight = 0m;
            Cost = 0m;
            Status = StatusType.Unknown;
            ErrorMessage = "";
        }

        public CalculationResults(string zip, decimal weight)
        {

        }
    }
}