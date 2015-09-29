using System;
using Suncrest.ShippingCalculator.Models;

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
    /// Defines the interface for calculation results which will include the inputs, weight and status
    /// </summary>
    public interface ICalculationResults
    {
        decimal Cost { get; set; }
        string ErrorMessage { get; set; }
        StatusType Status { get; set; }
        decimal Weight { get; set; }
        string ZipCode { get; set; }
        string Zone { get; set; }
    }
}
