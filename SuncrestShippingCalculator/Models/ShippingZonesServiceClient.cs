using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Configuration;
using System;

namespace Suncrest.ShippingCalculator.Models
{
    public class ShippingZonesServiceClient : IShippingZonesServiceClient
    {
        private static readonly Lazy<ShippingZonesServiceClient> lazy = new Lazy<ShippingZonesServiceClient>(() => new ShippingZonesServiceClient());
        public static ShippingZonesServiceClient Instance { get { return lazy.Value; } }

        private string _shippingServiceUri;
        private IRestClient _client;

        private ShippingZonesServiceClient()
        {
            _shippingServiceUri = ConfigurationManager.AppSettings["ShippingServiceUri"];
            if (_shippingServiceUri == null)
            {
                throw new KeyNotFoundException("ShippingServiceUri key not found in AppSettings");
            }
            _client = new RestClient(_shippingServiceUri);
        }

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
    }
}