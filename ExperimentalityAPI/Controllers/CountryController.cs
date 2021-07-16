using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Description;
using ExperimentalityAPI.Models;
using ExperimentalityAPI.Models.Country;
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

        /// <summary>
        /// Endpoint to register countries and its maximun percentage of discount
        /// </summary>
        /// <param name="country">CountryCreate{name, maxDiscountPercentage}</param>
        /// <returns>ResultOperationProject{stateOperation,messageResult,Country}</returns>
        [HttpPost("AddCountry")]
        public async Task<ResultOperationProject<Country>> AddCountry([FromForm]CountryCreate country)
        {
           return await _service.AddCountryAsync(country);
        }

        /// <summary>
        /// Endpoint to update countries name and its maximun percentage of discount
        /// </summary>
        /// <param name="country">CountryCreate{name, maxDiscountPercentage,id}</param>
        /// <returns>ResultOperationProject{stateOperation,messageResult,Country}</returns>
        [HttpPut("UpdateCountry")]
        public async Task<ResultOperationProject<Country>> Update([FromForm]CountryUpdate country)
        {
            return await _service.Update(country);
        }

        /// <summary>
        /// Endpoint to delete countries and its maximun percentage of discount
        /// </summary>
        /// <param name="id">id string</param>
        /// <returns>ResultOperationProject{stateOperation,messageResult,Country}</returns>
        [HttpDelete("DeleteCountry")]
        public async Task<ResultOperationProject<Country>> Delete([FromForm]string id)
        {
            return await _service.Delete(id);
        }

        /// <summary>
        /// Edpoint to get all contries and its maximun percentage of discount
        /// </summary>
        /// <returns>ResultOperationProject{stateOperation,messageResult,Country list}</returns>
        [HttpGet("GetCountry")]
        public async Task<ResultOperationProject<Country>> Get()
        {
            return await _service.Get();
        }
    }
}