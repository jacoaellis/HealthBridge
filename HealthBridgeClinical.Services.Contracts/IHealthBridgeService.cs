using HealthBridgeClinical.Models.DTOs;

namespace HealthBridgeClinical.Services.Contracts
{
    public interface IHealthBridgeService
    {
        ContinentsDto GetContinents();
        CountriesDto GetCountries();
    }
}
