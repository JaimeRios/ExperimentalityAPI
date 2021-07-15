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
using ExperimentalityAPI.Models.Product;

namespace ExperimentalityAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _service;
        /// <summary>
        /// Constructor ProductController
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="service"></param>
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
        [ProducesResponseType(typeof(ResultOperationProject<Product>), 200)]
        public async Task<ResultOperationProject<Product>> AddProduct([FromForm] ProductCreate product)
        {
            return await _service.AddProduct(product);
        }

        /// <summary>
        /// GetAll Products
        /// </summary>
        /// <returns></returns>
        [HttpGet("getProductData")]
        public IEnumerable<Product> Get()
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