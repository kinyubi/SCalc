using System;
namespace Suncrest.ShippingCalculator.Models
{
    /// <summary>
    /// Reqired inputs for a calculation. Zone is not included because it is an intermediate value
    /// that may or may not be required for all calculation methods.
    /// We are defining the calculations as COST = f(ZIPCODE, WEIGHT)
    /// </summary>
    public interface ICalculationInputs
    {
        decimal Weight { get; set; }
        string ZipCode { get; set; }
    }
}
