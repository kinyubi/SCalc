using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Configuration;
using System;

namespace Suncrest.ShippingCalculator.Models
{
    public class DefaultShippingZoneLookup : IShippingZonesLookup
    {

        /// <summary>
        /// Implementation of a RestSharp library RESTful service client
        /// </summary>
        private IRestClient _client;

        public string ServiceUri { get; private set; }

        public DefaultShippingZoneLookup(IConfigurationWrapper configSettings)
        {
            if (!configSettings.HasKey("ShippingServiceUri"))
            {
                throw new KeyNotFoundException("ShippingServiceUri key not found in configuration");
            }
            ServiceUri = configSettings.GetValue("ShippingServiceUri");
            _client = new RestClient(ServiceUri);
        }

        public DefaultShippingZoneLookup() : this(DefaultConfigurationWrapper.Instance)
        {
        }

        /// <summary>
        /// Gets the ShippingZone object associated with the zip parameter.
        /// </summary>
        /// <param name="zip">a zipcode.</param>
        /// <returns>
        /// should return a ShippingZone object or null if none found
        /// </returns>
        /// <exception cref="System.Net.WebException">Unexpected response from ShippingZonesService</exception>
        public IShippingZoneLookupEntry GetOne(string zip)
        {
            var request = new RestRequest("Zones/{zip}", Method.GET) { RequestFormat = DataFormat.Json };
            request.AddParameter("zip", zip, ParameterType.UrlSegment);
            var response = _client.Execute<ShippingZoneLookupEntry>(request);
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
        public IEnumerable<IShippingZoneLookupEntry> GetAll()
        {
            var request = new RestRequest("Zones", Method.GET) { RequestFormat = DataFormat.Json };
            var response = _client.Execute<List<ShippingZoneLookupEntry>>(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new WebException("Unexpected response from ShippingZonesService");
            }
            return response.Data;
        }
    }
}