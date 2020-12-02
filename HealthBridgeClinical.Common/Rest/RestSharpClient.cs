using HealthBridgeClinical.Common.Exceptions;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace HealthBridgeClinical.Common.Rest
{
    public class RestSharpClient : IRestClient
    {

        public async Task<T> GetAsync<T>(string url)
        {
            var headerDict = new Dictionary<string, string>();
            return await GetAsync<T>(url, headerDict);
        }
        public async Task<T> GetAsync<T>(string url, Dictionary<string, string> headers)
        {
            var restClient = new RestClient(url);
            var request = BuildRequest(Method.GET, headers);
            var response = await restClient.ExecuteAsync<T>(request);
            CatchHttpExceptions(url, request, response);
            return response.Data;
        }
        private static RestRequest BuildRequest(Method method, Dictionary<string, string> headers)
        {
            var request = new RestRequest(method);
            foreach (KeyValuePair<string, string> header in headers)
            {
                request.AddHeader(header.Key, header.Value);
            }
            return request;
        }

        private static void CatchHttpExceptions(string url, IRestRequest request, IRestResponse response)
        {
            if (response.IsSuccessful)
                return;

            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new NotFoundException();

            var message = $"{{ \"Request\": \"{url}\", \"Status code\": {response.StatusCode}, \"Response\": \"{response.Content}\" }}";

            if (response.ErrorException != null)
                throw new RestClientException(message, response.ErrorException);

            if (response.StatusCode == HttpStatusCode.OK)
                return;

            throw new RestClientException(message);
        }

    }
}
