using System;
using System.Text.RegularExpressions;
using System.Configuration;

namespace Suncrest.ShippingCalculator.Models
{

    /// <summary>
    /// Represents the shipping calculation and includes inputs, outputs and status of the calculation
    /// </summary>
    public class Calculator
    {
        /// <summary>
        /// The ShippingCost object returned by the shipping cost service client
        /// </summary>
        private IShippingCostLookupEntry _costTier;

        /// <summary>
        /// The ShippingZone object returned the the shipping zone service client.
        /// </summary>
        private IShippingZoneLookupEntry _zipZone;

        private IShippingCostsLookup _costLookupClient;

        private IShippingZonesLookup _zoneLookupClient;

        /// <summary>
        /// Gets or sets the inputs used to make the shipping cost calculation.
        /// </summary>
        public ICalculationInputs Inputs {get; set; }

        /// <summary>
        /// Gets or sets the CalculationResults object which represents the calculation result.
        /// </summary>
        public ICalculationResults Results { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Calculator"/> class.
        /// </summary>
        public Calculator(IShippingZonesLookup zoneLookupClient, IShippingCostsLookup costLookupClient)
        {
            _costLookupClient = costLookupClient;
            _zoneLookupClient = zoneLookupClient;
            Results = new CalculationResults();
        }

        /// <summary>
        /// Computes the shipping cost based on the inputs. Creates the Results object.
        /// </summary>
        /// <param name="zip">The zip.</param>
        /// <param name="weight">The weight.</param>
        public ICalculationResults Compute(string zip, decimal weight)
        {
            Inputs = new CalculationInputs(zip,weight);
            Results.ZipCode = zip;
            Results.Weight = weight;
            DoCompute();
            return Results;
        }

        /// <summary>
        /// Computes the shipping cost based on the inputs. Creates the Results object.
        /// </summary>
        /// <param name="inputs">The input object containing a zipcode and a weight</param>
        public ICalculationResults Compute(CalculationInputs inputs)
        { 
            return Compute(inputs.ZipCode, inputs.Weight);
        }

        /// <summary>
        /// Computes the shipping cost based on the inputs. This assumes the Inputs object
        /// has already been created and returns on exception if it hasn't. Creates the Results object. 
        /// </summary>
        /// <exception cref="System.NullReferenceException">CalculationInput object was null</exception>
        public ICalculationResults Compute()
        {
            if (Inputs == null)
            {
                throw new NullReferenceException("CalculationInput object was null");
            }
            DoCompute();
            return Results;
        }

        /// <summary>
        /// Does the computation once the Inputs object has been set. Validates the inputs.
        /// The exceptions thrown are handled by the by the method and are reflected in 
        /// the value of the Status and ErrorMessage properties in the Results object.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// zip;Zip code must be 5 digits
        /// or
        /// zip;Zone not available for specified zip code
        /// or
        /// weight;Weight must be greater than 0 and less than 100 lbs
        /// or
        /// weight;Shipping cost not available for the specified zone and weight
        /// </exception>
        private void DoCompute()
        { 
            try 
	        {	        
		        if (!Regex.IsMatch(Inputs.ZipCode,  @"^\d{5}$"))
                {
                    Results.Status = StatusType.InvalidZipCode;
                    Results.ErrorMessage = String.Format("{0} is an invalid zipcode specification. Must be five digits.", Inputs.ZipCode);
                    throw new ArgumentOutOfRangeException("zip", Inputs.ZipCode, "Zip code must be 5 digits");
                }
                //determine the zone for the specified zone by calling the service
                _zipZone = _zoneLookupClient.GetOne(Inputs.ZipCode);
                if (_zipZone == null)
                {
                    Results.Status = StatusType.ZoneUnknownForSpecifiedZipCode;
                    Results.ErrorMessage = String.Format("The zone associated with zipcode {0} is not on file.", Inputs.ZipCode);
                    throw new ArgumentOutOfRangeException("zip", Inputs.ZipCode, "Zone not available for specified zip code");
                }
                Results.Zone = _zipZone.Zone;
                if (Inputs.Weight < 0.01m || Inputs.Weight > 99.99m )
                {
                    Results.Status = StatusType.InvalidWeight;
                    Results.ErrorMessage = String.Format("{0} is not a valid weight. The weight must be a positive number less than 100.", Inputs.Weight);
                    throw new ArgumentOutOfRangeException("weight", Inputs.Weight, "Weight must be greater than 0 and less than 100 lbs");
                }

                _costTier = _costLookupClient.GetOne(Results.Zone, Inputs.Weight);

                if (_costTier == null)
                {
                    Results.Status = StatusType.CostUnknownForZipCodeAndWeight;
                    Results.ErrorMessage = String.Format("Zipcode {0} is in zone {1}. There is no shipping cost information on file for a package weighing {2} lbs in that zone",Inputs.ZipCode, Results.Zone, Inputs.Weight);
                    throw new ArgumentOutOfRangeException("weight",Inputs.Weight, "Shipping cost not available for the specified zone and weight");
                }
                Results.Cost = _costTier.Cost;
                Results.Status = StatusType.Success;
	        }
	        catch (ArgumentOutOfRangeException ex)
	        {
                if (Results.Status == StatusType.Unknown)
                {
                    Results.Status = StatusType.UnknownError;
                    Results.ErrorMessage = ex.Message;
                }
	        }
            catch (Exception ex)
            {
                Results.Status = StatusType.UnknownError;
                Results.ErrorMessage = ex.Message;
            }
        }
    }
}