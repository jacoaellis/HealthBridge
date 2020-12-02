using HealthBridgeClinical.Common.Rest;
using HealthBridgeClinical.Models.DTOs;
using HealthBridgeClinical.Services.Contracts;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthBridgeClinical.Services
{
    public class HealthBridgeService : IHealthBridgeService
    {
        private readonly string _baseUrl;
        private readonly IRestClient _restClient;
        private readonly Dictionary<string, string> _headers;

        public HealthBridgeService(IConfiguration configuration, IRestClient restClient)
        {
            _restClient = restClient;
            _baseUrl = configuration["RapidApiURL"];
            _headers = new Dictionary<string, string>
            {
                { "x-rapidapi-key", configuration["RapidApiKey"] },
                { "x-rapidapi-host",configuration["RapidApiHost"] }
            };

        }

        public ContinentsDto GetContinents()
        {
            var url = $"{_baseUrl}/countries";
            var resopnse = _restClient.Get<Task>(url, _headers);
            // from the data that returns logic needs to be applied to strip the Contries 
            // call GetStatistics with a "?country=Afghanistan" appended parameter to get continent 
            // build a CountriesDto and group it by continent
            var data = new ContinentsDto
            {
                Continent = resopnse.Content
            };

            return data;
        }

        public CountriesDto GetCountries()
        {
            var url = $"{_baseUrl}/countries";
            var resopnse = _restClient.Get<Task>(url, _headers);
            // from the data that returns logic needs to be applied to strip the Contries 
            // call GetStatistics with a "?country=Afghanistan" appended parameter to get the statistics and return it into the results nitpikking [cases] for active and new, [deaths]
            var data = new CountriesDto
            {
                Country = resopnse.Content
            };

            return data;
        }

        public CountriesDto GetStatistics(string country)
        {
            var url = $"{_baseUrl}/statistics?country={country}";
            var resopnse = _restClient.Get<Task>(url, _headers);
            var data = new CountriesDto
            {
                Continent = resopnse.Content
            };

            return data;
        }

    }
}
