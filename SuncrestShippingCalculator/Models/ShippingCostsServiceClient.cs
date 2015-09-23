using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Configuration;
using System;

namespace Suncrest.ShippingCalculator.Models
{
    /// <summary>
    /// Singleton representing a concrete implementation of IShippingCostsServiceClient.
    /// </summary>
    public sealed class ShippingCostsServiceClient : IShippingCostsServiceClient
    {
        /// <summary>
        /// Part of the singleton pattern implementation
        /// </summary>
        private static readonly Lazy<ShippingCostsServiceClient> lazy = new Lazy<ShippingCostsServiceClient>(() => new ShippingCostsServiceClient());

        /// <summary>
        /// Gets the ShippingCostsServiceClient instance.
        /// </summary>
        public static ShippingCostsServiceClient Instance {get {return lazy.Value;}}

        /// <summary>
        /// The ShippingServiceUri appsetting found in the Web.Config file.
        /// </summary>
        private string _shippingServiceUri;

        /// <summary>
        /// Implementation of a RestSharp library RESTful service client
        /// </summary>
        private IRestClient _client;

        /// <summary>
        /// Prevents a default instance of the <see cref="ShippingCostsServiceClient"/> class from being created.
        /// </summary>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">ShippingServiceUri key not found in AppSettings</exception>
        private ShippingCostsServiceClient()
        {
            _shippingServiceUri = ConfigurationManager.AppSettings["ShippingServiceUri"];
            if (ServiceUri == null)
            {
                throw new KeyNotFoundException("ShippingServiceUri key not found in AppSettings");
            }
            _client = new RestClient(ServiceUri);
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
        public ShippingCost GetOne(string zone, decimal weight)
        {
            var request = new RestRequest("Costs/{zone}/{weight}/", Method.GET) { RequestFormat = DataFormat.Json };
            request.AddParameter("zone", zone, ParameterType.UrlSegment);
            request.AddParameter("weight", weight, ParameterType.UrlSegment);
            var response = _client.Execute<ShippingCost>(request);
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
        public IEnumerable<ShippingCost> GetAll()
        {
            var request = new RestRequest("Costs", Method.GET) { RequestFormat = DataFormat.Json };
            var response = _client.Execute<List<ShippingCost>>(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new WebException("Unexpected response from ShippingService");
            }
            return response.Data;
        }

        public string ServiceUri { get { return _shippingServiceUri; } }
    }
}