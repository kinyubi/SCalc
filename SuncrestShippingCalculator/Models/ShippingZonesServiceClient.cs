using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Configuration;
using System;

namespace Suncrest.ShippingCalculator.Models
{
    /// <summary>
    /// Singleton representing a concrete implementation of IShippingZonesServiceClient.
    /// </summary>
    public class ShippingZonesServiceClient : IShippingZonesServiceClient
    {
        /// <summary>
        /// Part of the singleton pattern implementation
        /// </summary>
        private static readonly Lazy<ShippingZonesServiceClient> lazy = new Lazy<ShippingZonesServiceClient>(() => new ShippingZonesServiceClient());

        /// <summary>
        /// Gets the ShippingCostsServiceClient instance.
        /// </summary>
        public static ShippingZonesServiceClient Instance { get { return lazy.Value; } }

        /// <summary>
        /// The ShippingServiceUri appsetting found in the Web.Config file.
        /// </summary>
        private string _shippingServiceUri;

        /// <summary>
        /// Implementation of a RestSharp library RESTful service client
        /// </summary>
        private IRestClient _client;

        /// <summary>
        /// Prevents a default instance of the <see cref="ShippingZonesServiceClient"/> class from being created.
        /// </summary>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">ShippingServiceUri key not found in AppSettings</exception>
        private ShippingZonesServiceClient()
        {
            _shippingServiceUri = ConfigurationManager.AppSettings["ShippingServiceUri"];
            if (ServiceUri == null)
            {
                throw new KeyNotFoundException("ShippingServiceUri key not found in AppSettings");
            }
            _client = new RestClient(ServiceUri);
        }

        /// <summary>
        /// Gets the ShippingZone object associated with the zip parameter.
        /// </summary>
        /// <param name="zip">a zipcode.</param>
        /// <returns>
        /// should return a ShippingZone object or null if none found
        /// </returns>
        /// <exception cref="System.Net.WebException">Unexpected response from ShippingZonesService</exception>
        public ShippingZone GetOne(string zip)
        {
            var request = new RestRequest("Zones/{zip}", Method.GET) { RequestFormat = DataFormat.Json };
            request.AddParameter("zip", zip, ParameterType.UrlSegment);
            var response = _client.Execute<ShippingZone>(request);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new WebException("Unexpected response from ShippingZonesService");
            }
            return response.Data;
        }

        /// <summary>
        /// Gets the entire array of ShippingZone objects.
        /// </summary>
        /// <returns>
        /// an entire array of ShippingZone objects
        /// </returns>
        /// <exception cref="System.Net.WebException">Unexpected response from ShippingZonesService</exception>
        public IEnumerable<ShippingZone> GetAll()
        {
            var request = new RestRequest("Zones", Method.GET) { RequestFormat = DataFormat.Json };
            var response = _client.Execute<List<ShippingZone>>(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new WebException("Unexpected response from ShippingZonesService");
            }
            return response.Data;
        }

        public string ServiceUri { get { return _shippingServiceUri; } }
    }
}