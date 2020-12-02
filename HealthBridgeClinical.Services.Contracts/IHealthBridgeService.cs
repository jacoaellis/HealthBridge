using HealthBridgeClinical.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthBridgeClinical.Services.Contracts
{
    public interface IHealthBridgeService
    {
        Task<List<ContinentsDto>> GetContinents();
        Task<List<CountriesDto>> GetCountries();
    }
}
