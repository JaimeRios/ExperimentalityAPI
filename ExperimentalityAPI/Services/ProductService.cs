using ExperimentalityAPI.Models;
using ExperimentalityAPI.Models.Country;
using ExperimentalityAPI.Models.Product;
using ExperimentalityAPI.Repository.Interfaces;
using ExperimentalityAPI.Services.Interfaces;
using ExperimentalityAPI.Utils.ImageUtils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExperimentalityAPI.Services
{
    public class ProductService: IProductService
    {
        private readonly IMongoRepository<Product> _repository;
        private readonly IMongoRepository<Country> _countryrepository;

        public ProductService(IMongoRepository<Product> repository, IMongoRepository<Country> countryrepository)
        {
            _repository = repository;
            _countryrepository = countryrepository;
        }

        public async Task<ResultOperationProject<Product>> AddProduct(ProductCreate product)
        {
            ResultOperationProject<Product> result = new ResultOperationProject<Product>();
            result.result = null;
            try
            {
                if (ValidateMaxPercentageByCountry(product.country, product.discountPercentage))
                {
                    JToken jAppSettings = JToken.Parse(
                      File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "appsettings.json"))
                    );

                    var ImageFormats = jAppSettings["ImageSettings"]["FormatAllowed"].ToString().Split(',').ToList(); ;
                    if(ImageFormats.Contains(product.frontImage.FileName.Split('.').ElementAt(1))&&
                        ImageFormats.Contains(product.backImage.FileName.Split('.').ElementAt(1)))
                    {

                        if (_repository.FilterBy(p => p.name == product.name).FirstOrDefault() == null)
                        {
                            if (product.discountPercentage < 0 || product.discountPercentage > 100)
                            {
                                result.messageResult = $"Maximun percentage of discont Must be a value betwen 0-100.";
                                result.stateOperation = true;
                            }
                            else
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

                                Product newData = new Product();
                                newData.fromProductCreate(product);
                                await _repository.InsertOneAsync(newData);
                                result.result = _repository.FilterBy(p => p.name == product.name).FirstOrDefault();
                                result.messageResult = $"Element created successfully.";
                                result.stateOperation = true;
                            }

                        }
                        else
                        {
                            result.messageResult = $"Already exist a product named {product.name}.";
                            result.stateOperation = true;
                        }

                        
                    }
                    else
                    {
                        result.stateOperation = false;
                        result.messageResult = $" '.{product.frontImage.FileName.Split('.').ElementAt(1)}' or '.{product.backImage.FileName.Split('.').ElementAt(1)}' is not a allowed extension.";
                    }
                    
                }
                else
                {
                    result.stateOperation = false;
                    result.messageResult = $"Country {product.country} does not allow {product.discountPercentage}% discount percentage";
                }
                
            }
            catch (Exception exc)
            {
                result.stateOperation = false;
                result.messageResult = exc.Message;
            }
            return result;
        }

        public IEnumerable<Product> Get()
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
