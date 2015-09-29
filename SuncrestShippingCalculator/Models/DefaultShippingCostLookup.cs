using RestSharp;
using System.Collections.Generic;
using System.Net;
using System;

namespace Suncrest.ShippingCalculator.Models
{
    /// <summary>
    /// Default implementation of IShippingCostsServiceClient.
    /// </summary>
    public class DefaultShippingCostLookup : IShippingCostsLookup
    {

        /// <summary>
        /// Implementation of a RestSharp library RESTful service client
        /// </summary>
        private IRestClient _client;

        /// <summary>
        /// The ShippingServiceUri appsetting found in the Web.Config file.
        /// </summary>
        public string ServiceUri { get; private set; }

        /// <summary>
        /// Create an instance of the <see cref="DefaultShippingCostLookup"/> class.
        /// </summary>
        public DefaultShippingCostLookup(IConfigurationWrapper configSettings)
        {
            if (!configSettings.HasKey("ShippingServiceUri"))
            {
                throw new KeyNotFoundException("ShippingServiceUri key not found in configuration");
            }
            ServiceUri = configSettings.GetValue("ShippingServiceUri");
            _client = new RestClient(ServiceUri);
        }

        public DefaultShippingCostLookup() : this(DefaultConfigurationWrapper.Instance)
        {
        }

        /// <summary>
        /// Gets the ShippingCost object based on zone and weight.
        /// </summary>
        /// <param name="zone">The zone.</param>
        /// <param name="weight">The weight.</param>
        /// <returns>
        /// The associated ShippingCost object or null if not found
        /// </returns>
        /// <exception cref="System.Net.WebException">Unexpected response from ShippingCostsService</exception>
        public IShippingCostLookupEntry GetOne(string zone, decimal weight)
        {
            var request = new RestRequest("Costs/{zone}/{weight}/", Method.GET) { RequestFormat = DataFormat.Json };
            request.AddParameter("zone", zone, ParameterType.UrlSegment);
            request.AddParameter("weight", weight, ParameterType.UrlSegment);
            var response = _client.Execute<ShippingCostLookupEntry>(request);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new WebException("Unexpected response from ShippingCostsService");
            }
            return response.Data;
        }

        /// <summary>
        /// Gets the entire array of ShippingCost objects.
        /// </summary>
        /// <returns>
        /// The entire table of ShippingCost objects
        /// </returns>
        /// <exception cref="System.Net.WebException">Unexpected response from ShippingService</exception>
        public IEnumerable<IShippingCostLookupEntry> GetAll()
        {
            var request = new RestRequest("Costs", Method.GET) { RequestFormat = DataFormat.Json };
            var response = _client.Execute<List<ShippingCostLookupEntry>>(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new WebException("Unexpected response from ShippingService");
            }
            return response.Data;
        }

    }
}