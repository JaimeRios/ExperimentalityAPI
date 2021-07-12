using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Model;

namespace ExperimentalityAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Segunda prueba
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("load")]
        public IActionResult Upload(IFormFile file)
        {
            return Ok();
        }

        [HttpPost]
        [Route("createProduct")]
        public IActionResult createProduct([FromForm] Product product)
        {
            return Ok();
        }
    }
}