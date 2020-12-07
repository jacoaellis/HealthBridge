using HealthBridgeClinical.Models.DTOs;
using HealthBridgeClinical.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HealthBridgeClinical.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class CovidController : ControllerBase
    {
        private readonly IHealthBridgeService _hbs;

        public CovidController(IHealthBridgeService healthBridgeService)
        {
            _hbs = healthBridgeService;
        }

        [HttpGet("continents")]
        [ProducesResponseType(typeof(ContinentsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetContinents()
        {
            var serviceResponse = _hbs.GetContinents();
            return Ok(serviceResponse);
        }

        [HttpGet("countries")]
        [ProducesResponseType(typeof(List<CountriesDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCountries()
        {
            var serviceResponse = _hbs.GetCountries();
            return Ok(serviceResponse);
        }
    }
}
