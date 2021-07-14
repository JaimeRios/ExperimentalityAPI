using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExperimentalityAPI.Models;
using ExperimentalityAPI.Repository.Interfaces;
using ExperimentalityAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExperimentalityAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ILogger<CountryController> _logger;
        private readonly ICountryService _service;

        public CountryController(ILogger<CountryController> logger,
            ICountryService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost("AddCountry")]
        public async Task AddCountry([FromForm]Country country)
        {

            await _service.AddCountry(country);
        }

        [HttpPut("UpdateCountry")]
        public async Task Update([FromForm]Country country)
        {
            await _service.Update(country);
        }

        [HttpDelete("DeleteCountry")]
        public async Task Delete([FromForm]string id)
        {
            await _service.Delete(id);
        }

        [HttpGet("GetCountry")]
        public List<Country> Get()
        {
            return _service.Get();
        }
    }
}