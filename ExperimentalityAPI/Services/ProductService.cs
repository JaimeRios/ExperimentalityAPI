using ExperimentalityAPI.Models;
using ExperimentalityAPI.Models.Country;
using ExperimentalityAPI.Models.Product;
using ExperimentalityAPI.Repository.Interfaces;
using ExperimentalityAPI.Services.Interfaces;
using ExperimentalityAPI.Utils.ImageUtils;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExperimentalityAPI.Services
{
    /// <summary>
    /// Service fo Model Product
    /// </summary>
    public class ProductService: IProductService
    {
        private readonly IMongoRepository<Product> _repository;
        private readonly IMongoRepository<Country> _countryrepository;

        /// <summary>
        /// Product service constructor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="countryrepository"></param>
        public ProductService(IMongoRepository<Product> repository, IMongoRepository<Country> countryrepository)
        {
            _repository = repository;
            _countryrepository = countryrepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<ResultOperationProject<Product>> Update(ProductUpdate product)
        {
            ResultOperationProject<Product> result = new ResultOperationProject<Product>();
            result.result = null;
            try
            {
                if(ValidateMaxPercentageByCountry(product.country, product.discountPercentage)) 
                {
                    JToken jAppSettings = JToken.Parse(
                      File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "appsettings.json"))
                    );

                    var ImageFormats = jAppSettings["ImageSettings"]["FormatAllowed"].ToString().Split(',').ToList(); ;
                    if (ImageFormats.Contains(product.frontImage.FileName.Split('.').ElementAt(1)) &&
                        ImageFormats.Contains(product.backImage.FileName.Split('.').ElementAt(1)))
                    {

                        Product _productId = new Product();
                        _productId.Id = new ObjectId(product.id);
                        if(_repository.FilterBy(p => p.name == product.name && p.Id != _productId.Id).FirstOrDefault() == null)
                        {
                            var productForUpdate = _repository.FindById(product.id);
                            if (productForUpdate.Id != null)
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

                                productForUpdate.fromProductUpdate(product);
                                await _repository.ReplaceOneAsync(productForUpdate);
                                result.result = productForUpdate;
                                result.messageResult = $"Element updated successfully.";
                                result.stateOperation = true;
                            }
                            else
                            {
                                result.messageResult = $"Does not exist a product whit id '{product.id}'.";
                                result.stateOperation = false;
                            }

                        }
                        else
                        {
                            result.messageResult = $"Product element whit name '{product.name}' Duplicate.";
                            result.stateOperation = false;
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
                if (exc.Message == "Index was outside the bounds of the array.")
                {
                    result.stateOperation = false;
                    result.messageResult = $"Does not exist a product whit id '{product.id}'.";
                }
                else
                {
                    result.stateOperation = false;
                    result.messageResult = exc.Message;
                }
            }
            return result;
        }

        public async Task<ResultOperationProject<Product>> Delete(string id) 
        {
            ResultOperationProject<Product> result = new ResultOperationProject<Product>();
            result.result = null;
            try
            {
                var ProductForDelete = await _repository.FindByIdAsync(id);
                if (ProductForDelete != null)
                {
                     _repository.DeleteById(id);
                    result.result = ProductForDelete;
                    result.messageResult = $"Element deleted successfully. ";
                    result.stateOperation = true;
                }
                else
                {
                    result.messageResult = $"Does not exist a Product whit id '{id}'.";
                    result.stateOperation = true;
                }


            }
            catch (Exception exc)
            {
                if (exc.Message == "Index was outside the bounds of the array.")
                {
                    result.stateOperation = false;
                    result.messageResult = $"Does not exist a Product whit id '{id}'.";
                }
                else
                {
                    result.stateOperation = false;
                    result.messageResult = exc.Message;
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<ResultOperationProject<Product>> Get()
        {
            ResultOperationProject<Product> result = new ResultOperationProject<Product>();
            result.result = null;
            try
            {
                result.results = _repository.AsQueryable().ToList();
                result.messageResult = $"There are a total {result.results.Count} of Products registered.";
                result.stateOperation = true;

            }
            catch (Exception exc)
            {
                result.stateOperation = false;
                result.messageResult = exc.Message;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<ResultOperationProject<Product>> GetByName(string name) 
        {
            ResultOperationProject<Product> result = new ResultOperationProject<Product>();
            result.result = null;
            try
            {
                var productSearched = _repository.FilterBy(p => p.name == name).FirstOrDefault();
                if (productSearched != null)
                {
                    productSearched.consulted++;

                    await _repository.ReplaceOneAsync(productSearched);
                    result.result = productSearched;
                    result.messageResult = $"Element exist!!. ";
                    result.stateOperation = true;
                }
                else
                {
                    result.messageResult = $"Does not exist a Product whit name '{name}'.";
                    result.stateOperation = true;
                }


            }
            catch (Exception exc)
            {
                if (exc.Message == "Index was outside the bounds of the array.")
                {
                    result.stateOperation = false;
                    result.messageResult = $"Does not exist a Product whit name '{name}'.";
                }
                else
                {
                    result.stateOperation = false;
                    result.messageResult = exc.Message;
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<ResultOperationProject<Product>> GetMoreSearched(int count)
        {
            ResultOperationProject<Product> result = new ResultOperationProject<Product>();
            result.result = null;
            try
            {
                List<Product> data = new List<Product>();
                data = _repository.AsQueryable().ToList().OrderByDescending(p=> p.consulted).ToList();
                if (data.Count != 0) 
                {
                    if (data.Count < count)
                    {
                        result.results = data;
                        result.messageResult = $"There are a total {result.results.Count} of Products registered.";
                    }
                    else
                    {
                        result.results = data.GetRange(0, count);
                        result.messageResult = $"The {result.results.Count} most consulted product by the name.";
                    }
                    
                    result.stateOperation = true;
                }
                else
                {
                    result.messageResult = $"There are 0 products in database.";
                    result.stateOperation = true;
                }
            }
            catch (Exception exc)
            {
                result.stateOperation = false;
                result.messageResult = exc.Message;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="country"></param>
        /// <param name="percentage"></param>
        /// <returns></returns>
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
