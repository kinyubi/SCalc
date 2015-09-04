using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Suncrest.ShippingCalculator.Models;
using System.Configuration;

namespace Suncrest.ShippingCalculator.Models
{
    /// <summary>
    /// Based on the ShippingServiceUri appsetting in Web.Config, provides static 
    /// methods to retrieve the proper implementation of the IShippingZonesServiceClient
    /// and IShippingCostsServiceClient
    /// </summary>
    public class ServiceFactory
    {
        /// <summary>
        /// The ShippingServiceUri retrieved from Web.Config. Initialized by constructor.
        /// </summary>
        static string _shippingServiceUri;

        /// <summary>
        /// If the ShippingServiceUri contains the value 'Fake' then the FakeShippingZonesServiceClient
        /// is returned otherwise the URI is assumed to be a URI to a RESTful service and the
        /// ShippingZonesServiceClient is returned.
        /// </summary>
        /// <returns>an IShippingZonesServiceClient object</returns>
        /// <exception cref="System.Exception">ShippingServiceUri key not found in AppSettings</exception>
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

        /// <summary>
        /// If the ShippingServiceUri contains the value 'Fake' then the FakeShippingCostsServiceClient
        /// is returned otherwise the URI is assumed to be a URI to a RESTful service and the
        /// ShippingCostsServiceClient is returned.
        /// </summary>
        /// <returns>an IShippingCostsServiceClient object</returns>
        /// <exception cref="System.Exception">ShippingServiceUri key not found in AppSettings</exception>
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

        /// <summary>
        /// Initializes the <see cref="ServiceFactory"/> class.
        /// </summary>
        static ServiceFactory()
        {
            _shippingServiceUri = ConfigurationManager.AppSettings["ShippingServiceUri"];
        }
    }
}