using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Suncrest.ShippingCalculator.Models;
using System.Configuration;

namespace Suncrest.ShippingCalculator.Models
{
    public class ServiceFactory
    {
        static string _shippingServiceUri;

        public static IShippingZonesServiceClient GetZonesClient()
        {
            if (_shippingServiceUri == null)
            {
                throw new Exception("ShippingServiceUri key not found in AppSettings");
            }
            else if (_shippingServiceUri.ToLower() == "fake")
            {
                return FakeShippingZonesServiceClient.Instance;
            }
            else
            {
                return ShippingZonesServiceClient.Instance;
            }
        }

        public static IShippingCostsServiceClient GetCostsClient()
        {
            if (_shippingServiceUri == null)
            {
                throw new Exception("ShippingServiceUri key not found in AppSettings");
            }
            else if (_shippingServiceUri.ToLower() == "fake")
            {
                return FakeShippingCostServiceClient.Instance;
            }
            else
            {
                return ShippingCostsServiceClient.Instance;
            }
        }

        static ServiceFactory()
        {
            _shippingServiceUri = ConfigurationManager.AppSettings["ShippingServiceUri"];
        }
    }
}