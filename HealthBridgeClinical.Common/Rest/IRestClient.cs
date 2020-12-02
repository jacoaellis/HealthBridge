using RestSharp;
using System.Collections.Generic;

namespace HealthBridgeClinical.Common.Rest
{
    public interface IRestClient
    {
        IRestResponse Get<T>(string url, Dictionary<string, string> headers);

    }
}
