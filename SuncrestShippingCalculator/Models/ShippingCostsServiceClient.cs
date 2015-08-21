using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Web.Configuration;

namespace Suncrest.ShippingCalculator.Models
{
    public class ShippingCostsServiceClient
    {
        static string _shippingServiceUri;
        static IRestClient _client;

        static ShippingCostsServiceClient()
        {
            _shippingServiceUri = WebConfigurationManager.AppSettings["ShippingServiceUri"];
            if (_shippingServiceUri == null)
            {
                throw new KeyNotFoundException("ShippingServiceUri key not found in AppSettings");
            }
            _client = new RestClient(_shippingServiceUri);
        }

        public static ShippingCost GetOne(string zone, decimal weight)
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

        public static IEnumerable<ShippingCost> GetAll()
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