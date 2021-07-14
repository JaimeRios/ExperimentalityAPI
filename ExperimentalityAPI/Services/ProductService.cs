using ExperimentalityAPI.Models;
using ExperimentalityAPI.Repository.Interfaces;
using ExperimentalityAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExperimentalityAPI.Services
{
    public class ProductService: IProductService
    {
        private readonly IMongoRepository<ProductDB> _repository;

        public ProductService(IMongoRepository<ProductDB> repository)
        {
            _repository = repository;
        }

        public async Task AddProduct(Product product)
        {
            try
            {
                ProductDB newData = new ProductDB();
                newData.fromProduct(product);
                await _repository.InsertOneAsync(newData);
            }
            catch (Exception exc)
            {

                var message = exc.Message;
            }

        }

        public IEnumerable<ProductDB> Get()
        {
            var products = _repository.AsQueryable();
            return products;
        }
    }
}
