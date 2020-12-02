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

        public HealthBridgeService(IConfiguration configuration, IRestClient restClient)
        {
            _restClient = restClient;
            _baseUrl = configuration["MobileMartApiUrl"];
        }

        public Task<List<ContinentsDto>> GetContinents()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<CountriesDto>> GetCountries()
        {
            throw new System.NotImplementedException();
        }
    }
}
