using System;
using System.Text.RegularExpressions;
using System.Configuration;

namespace Suncrest.ShippingCalculator.Models
{

    public class ShippingCalculation
    {
        private ShippingCost _costTier;
        private ShippingZone _zipZone;

        public CalculationInputs Inputs {get; set; }
        public CalculationResults Results { get; set; }

        public ShippingCalculation() : this(null, 0m) {}

        public ShippingCalculation(string zip, decimal weight) 
        {
            Results = new CalculationResults
                {
                    ZipCode = zip,
                    Weight = weight
                };
            Inputs = new CalculationInputs(zip, weight);
            DoCompute();
        }

        public void Compute(string zip, decimal weight)
        {
            Inputs = new CalculationInputs(zip,weight);
            Results.ZipCode = zip;
            Results.Weight = weight;
            DoCompute();
        }

        public void Compute(CalculationInputs inputs)
        { 
            Compute(inputs.ZipCode, inputs.Weight);
        }

        public void Compute()
        {
            if (Inputs == null)
            {
                throw new NullReferenceException("CalculationInput object was null");
            }
            DoCompute();
        }

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
                IShippingZonesServiceClient _serviceClient = ServiceFactory.GetZonesClient();
                _zipZone = _serviceClient.GetOne(Inputs.ZipCode);
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

                IShippingCostsServiceClient _shippingCostServiceClient = ServiceFactory.GetCostsClient();
                _costTier = _shippingCostServiceClient.GetOne(Results.Zone, Inputs.Weight);

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