using HealthBridgeClinical.Common.Rest;
using HealthBridgeClinical.Models.DTOs;
using HealthBridgeClinical.Models.DTOs.Common;
using HealthBridgeClinical.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
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

        public List<ContinentsDto> GetContinents()
        {
            List<ContinentsDto> continentsDtos = new List<ContinentsDto>();

            List<CountriesDto> continents = GetCountries();
            foreach (var continent in continents.GroupBy(c => c.Continent).ToList())
            {
                if (continent.Key != null)
                {
                    ContinentsDto continentDto = new ContinentsDto();
                    continentDto.Continent = continent.Key;
                    continentDto.New = new CommonDataDto
                    {
                        Total = continent.Select(nt => nt.New.Total).Sum(),
                        Percent = continent.Select(nt => nt.New.Percent).Sum()
                    };
                    continentDto.Active = new CommonDataDto
                    {
                        Total = continent.Select(nt => nt.Active.Total).Sum(),
                        Percent = continent.Select(nt => nt.Active.Percent).Sum()
                    };
                    continentDto.Deaths = new CommonDataDto
                    {
                        Total = continent.Select(nt => nt.Deaths.Total).Sum(),
                        Percent = continent.Select(nt => nt.Deaths.Percent).Sum()
                    };
                    continentsDtos.Add(continentDto);
                }
            }

            return continentsDtos;
        }

        public List<CountriesDto> GetCountries()
        {
            List<CountriesDto> countriesDtos = new List<CountriesDto>();

            var url = $"{_baseUrl}/countries";
            var response = _restClient.Get<Task>(url, _headers);
            // from the data that returns logic needs to be applied to strip the countries 
            // call GetStatistics with a "?country=Afghanistan" appended parameter to get the statistics and return it into the results nitpikking [cases] for active and new, [deaths]

            JObject countriesContent = JObject.Parse(response.Content);
            var countriesList = countriesContent.SelectToken("response");

            foreach (var country in countriesList)
            {
                var data = GetStatistics(country.ToString());

                countriesDtos.Add(data);
            }

            countriesDtos.OrderBy(i => i.Continent).ThenBy(j => j.Country);

            return countriesDtos;
        }

        public CountriesDto GetStatistics(string country)
        {

            CountriesDto countryDtos = new CountriesDto();

            var url = $"{_baseUrl}/statistics?country={country}";
            var response = _restClient.Get<Task>(url, _headers);

            JObject content = JObject.Parse(response.Content);
            var countryResponse = content.SelectToken("response");

            if (countryResponse.HasValues)
            {
                countryDtos.Continent = countryResponse[0].SelectToken("continent").ToString();
                countryDtos.Country = countryResponse[0].SelectToken("country").ToString();
                countryDtos.New = new CommonDataDto
                {
                    Total = countryResponse[0].SelectToken("cases").SelectToken("new").Type != JTokenType.Null ?
                        (int)countryResponse[0].SelectToken("cases").SelectToken("new") : 0,
                    Percent = countryResponse[0].SelectToken("cases").SelectToken("new").Type != JTokenType.Null ?
                        (decimal)countryResponse[0].SelectToken("cases").SelectToken("new") / (decimal)countryResponse[0].SelectToken("cases").SelectToken("total") : 0
                };
                countryDtos.Active = new CommonDataDto
                {
                    Total = countryResponse[0].SelectToken("cases").SelectToken("active").Type != JTokenType.Null ?
                        (int)countryResponse[0].SelectToken("cases").SelectToken("active") : 0,
                    Percent = countryResponse[0].SelectToken("cases").SelectToken("active").Type != JTokenType.Null ?
                        (decimal)countryResponse[0].SelectToken("cases").SelectToken("active") / (decimal)countryResponse[0].SelectToken("cases").SelectToken("total") : 0
                };
                countryDtos.Deaths = new CommonDataDto
                {
                    Total = countryResponse[0].SelectToken("deaths").SelectToken("new").Type != JTokenType.Null ?
                        (int)countryResponse[0].SelectToken("deaths").SelectToken("new") : 0,
                    Percent = countryResponse[0].SelectToken("deaths").SelectToken("new").Type != JTokenType.Null ?
                        (decimal)countryResponse[0].SelectToken("deaths").SelectToken("new") / (decimal)countryResponse[0].SelectToken("cases").SelectToken("total") : 0
                };
            }

            return countryDtos;
        }

    }
}
