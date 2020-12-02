using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HealthBridgeClinical.Controllers
{
    [Route("[controller/v1]")]
    [ApiController]
    public class CovidController : ControllerBase
    {
        private readonly IHealthBridgeService _hbs;

        [HttpGet("continents")]
        [ProducesResponseType(typeof(ContinentsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetContinents()
        {
            var serviceResponse = await _hbs.GetContinents();
            return Ok(serviceResponse);
        }

        [HttpGet("countries")]
        [ProducesResponseType(typeof(CountriesDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCountries()
        {
            var serviceResponse = await _hbs.GetCountries();
            return Ok(serviceResponse);
        }
    }
}
