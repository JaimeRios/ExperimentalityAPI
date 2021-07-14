using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ExperimentalityAPI.Repository.Interfaces;
using System.IO;
using ExperimentalityAPI.Models;
using ExperimentalityAPI.Services.Interfaces;

namespace ExperimentalityAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _service;

        public ProductController(ILogger<ProductController> logger,
            IProductService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Register Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost("AddProduct")]
        public async Task AddProduct([FromForm] Product product)
        {
            await _service.AddProduct(product);
            
        }

        /// <summary>
        /// GetAll Products
        /// </summary>
        /// <returns></returns>
        [HttpGet("getProductData")]
        public IEnumerable<ProductDB> Get()
        {
            return _service.Get();
        }

        /// <summary>
        /// Create Products
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        //[HttpPost]
        //[Route("createProduct")]
        //public IActionResult createProduct([FromForm] Product product)
        //{
        //    return Ok();
        //}
    }
}