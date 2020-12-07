using HealthBridgeClinical.Models.DTOs;
using System.Collections.Generic;

namespace HealthBridgeClinical.Services.Contracts
{
    public interface IHealthBridgeService
    {
        ContinentsDto GetContinents();
        List<CountriesDto> GetCountries();
    }
}
