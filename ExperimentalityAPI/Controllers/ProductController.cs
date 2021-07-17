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
        private readonly IProductService _service;
        /// <summary>
        /// Constructor ProductController
        /// </summary>
        /// <param name="service"></param>
        public ProductController(IProductService service)
        {
            _service = service;
        }

        /// <summary>
        /// EndPoint to Register Products
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
        /// EndPoint to Update Products
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("UpdateProduct")]
        public async Task<ResultOperationProject<Product>> Update([FromForm]ProductUpdate product)
        {
            return await _service.Update(product);
        }

        /// <summary>
        /// EndPoint to Delette Products
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteProduct")]
        public async Task<ResultOperationProject<Product>> Delete([FromForm]string id)
        {
            return await _service.Delete(id);
        }

        /// <summary>
        /// EndPoint to GetAll Products
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllProducts")]
        public async Task<ResultOperationProject<Product>> GetAll()
        {
            return await _service.Get();
        }

        /// <summary>
        /// EndPoints to Get Product by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("GetProductByName")]
        public async Task<ResultOperationProject<Product>> GetByName(string name)
        {
            return await _service.GetByName(name);
        }

        /// <summary>
        /// EndPoints to Get specific acount product More Searched
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        [HttpGet("GetProductMoreSearched")]
        public async Task<ResultOperationProject<Product>> GetMoreSearched(int total)
        {
            return await _service.GetMoreSearched(total);
        }


    }
}