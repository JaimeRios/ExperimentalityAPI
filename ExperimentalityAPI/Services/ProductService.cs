using ExperimentalityAPI.Models;
using ExperimentalityAPI.Repository.Interfaces;
using ExperimentalityAPI.Services.Interfaces;
using ExperimentalityAPI.Utils.ImageUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExperimentalityAPI.Services
{
    public class ProductService: IProductService
    {
        private readonly IMongoRepository<ProductDB> _repository;
        private readonly IMongoRepository<Country> _countryrepository;

        public ProductService(IMongoRepository<ProductDB> repository, IMongoRepository<Country> countryrepository)
        {
            _repository = repository;
            _countryrepository = countryrepository;
        }

        public async Task AddProduct(Product product)
        {
            try
            {
                if (ValidateMaxPercentageByCountry(product.country, product.discountPercentage))
                {
                    if (product.frontImage.Length > 1048576)
                    {
                        ImageControlling image = new ImageControlling();
                        product.frontImage = image.resizeImage(product.frontImage);

                    }

                    if (product.backImage.Length > 1048576)
                    {
                        ImageControlling image = new ImageControlling();
                        product.backImage = image.resizeImage(product.backImage);

                    }

                    ProductDB newData = new ProductDB();
                    newData.fromProduct(product);
                    await _repository.InsertOneAsync(newData);
                }
                else
                {

                }
                
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

        public bool ValidateMaxPercentageByCountry(string country, int percentage) 
        {
            var countryList = _countryrepository.FilterBy(c => c.name == country).ToList();
            if (countryList.Count > 0)
            {
                if (countryList.ElementAt(0).maxDiscountPercentage >= percentage)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
    }
}
