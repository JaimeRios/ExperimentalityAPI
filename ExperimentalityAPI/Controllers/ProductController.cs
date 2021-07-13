using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ExperimentalityAPI.Model;
using ExperimentalityAPI.Repository.Interfaces;
using System.IO;
using ExperimentalityAPI.Models;

namespace ExperimentalityAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMongoRepository<ProductDB> _productsRepository;

        public ProductController(ILogger<ProductController> logger, 
            IMongoRepository<ProductDB> productRepository)
        {
            _logger = logger;
            _productsRepository = productRepository;
        }
        /// <summary>
        /// Register Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost("registerProduct")]
        public async Task AddPerson([FromForm] Product product)
        {
            try
            {
                ProductDB newData = new ProductDB();
                newData.fromProduct(product);
                await _productsRepository.InsertOneAsync(newData);
            }
            catch (Exception exc)
            {

                var message = exc.Message;
            }
            
        }

        /// <summary>
        /// GetAll Products
        /// </summary>
        /// <returns></returns>
        [HttpGet("getProductData")]
        public IEnumerable<ProductDB> GetPeopleData()
        {
            var products = _productsRepository.AsQueryable();/*.FilterBy(
                filter => filter.name != ""
            );*/
            return products;
        }

        /// <summary>
        /// Create Products
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("createProduct")]
        public IActionResult createProduct([FromForm] Product product)
        {
            return Ok();
        }
    }
}