using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Configuration;
using System;

namespace Suncrest.ShippingCalculator.Models
{
    //implement as singleton
    public sealed class ShippingCostsServiceClient : IShippingCostsServiceClient
    {
        private static readonly Lazy<ShippingCostsServiceClient> lazy = new Lazy<ShippingCostsServiceClient>(() => new ShippingCostsServiceClient());
        public static ShippingCostsServiceClient Instance {get {return lazy.Value;}}

        private string _shippingServiceUri;
        private IRestClient _client;

        private ShippingCostsServiceClient()
        {
            _shippingServiceUri = ConfigurationManager.AppSettings["ShippingServiceUri"];
            if (_shippingServiceUri == null)
            {
                throw new KeyNotFoundException("ShippingServiceUri key not found in AppSettings");
            }
            _client = new RestClient(_shippingServiceUri);
        }

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
    }
}