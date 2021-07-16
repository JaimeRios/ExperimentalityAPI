using ExperimentalityAPI.Models;
using ExperimentalityAPI.Models.Country;
using ExperimentalityAPI.Repository.Interfaces;
using ExperimentalityAPI.Services.Interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExperimentalityAPI.Services
{
    /// <summary>
    /// Service for Model Country
    /// </summary>
    public class CountryService: ICountryService
    {
        private readonly IMongoRepository<Country> _repository;

        /// <summary>
        /// Country Service Constructor 
        /// </summary>
        /// <param name="repository"></param>
        public CountryService(IMongoRepository<Country> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public async Task<ResultOperationProject<Country>> AddCountryAsync(CountryCreate country)
        {
            ResultOperationProject<Country> result = new ResultOperationProject<Country>();
            result.result = null;
            try
            {
                if(_repository.FilterBy(c => c.name == country.name).FirstOrDefault() == null)
                {
                    Country newDataCountry = new Country()
                    {
                        name = country.name,
                        maxDiscountPercentage = country.maxDiscountPercentage,
                    };
                    await _repository.InsertOneAsync(newDataCountry);
                    result.result = _repository.FilterBy(c => c.name == country.name).FirstOrDefault();
                    result.messageResult = $"Element created successfully.";
                    result.stateOperation = true;
                    
                }
                else
                {
                    result.messageResult = $"Already exist a country named {country.name}.";
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
        /// <returns></returns>
        public async Task<ResultOperationProject<Country>> Update(CountryUpdate country)
        {
            ResultOperationProject<Country> result = new ResultOperationProject<Country>();
            result.result = null;
            try
            {
                Country coun = new Country();
                coun.Id = new ObjectId(country.id);
                if(_repository.FilterBy(c => c.name == country.name && c.Id != coun.Id).FirstOrDefault()==null)
                {
                    var countryForUpdate = _repository.FindById(country.id);
                    if (countryForUpdate.Id != null)
                    {
                        countryForUpdate.maxDiscountPercentage = country.maxDiscountPercentage;
                        countryForUpdate.name = country.name;
                        await _repository.ReplaceOneAsync(countryForUpdate);
                        result.result = countryForUpdate;
                        result.messageResult = $"Element updated successfully.";
                        result.stateOperation = true;
                    }
                    else
                    {
                        result.messageResult = $"Does not exist a country whit id '{country.id}'.";
                        result.stateOperation = false;
                    }
                }
                else
                {
                    result.messageResult = $"Country element whit name '{country.name}' Duplicate.";
                    result.stateOperation = false;
                }
            }
            catch (Exception exc)
            {
                if(exc.Message== "Index was outside the bounds of the array.")
                {
                    result.stateOperation = false;
                    result.messageResult = $"Does not exist a country whit id '{country.id}'.";
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
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResultOperationProject<Country>> Delete(string id)
        {
            ResultOperationProject<Country> result = new ResultOperationProject<Country>();
            result.result = null;
            try
            {
                var countryForDelete = await _repository.FindByIdAsync(id);
                if (countryForDelete != null)
                {
                    await _repository.DeleteByIdAsync(id);
                    result.result = countryForDelete;
                    result.messageResult = $"Element deleted successfully. ";
                    result.stateOperation = true;
                }
                else
                {
                    result.messageResult = $"Does not exist a country whit id '{id}'.";
                    result.stateOperation = true;
                }

                
            }
            catch (Exception exc)
            {
                if (exc.Message == "Index was outside the bounds of the array.")
                {
                    result.stateOperation = false;
                    result.messageResult = $"Does not exist a country whit id '{id}'.";
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
        public async Task<ResultOperationProject<Country>> Get()
        {
            ResultOperationProject<Country> result = new ResultOperationProject<Country>();
            result.result = null;
            try
            {
                result.results = _repository.AsQueryable().ToList();
                result.messageResult = $"There are a total {result.results.Count} of countries registers.";
                result.stateOperation = true;

            }
            catch (Exception exc)
            {
                result.stateOperation = false;
                result.messageResult = exc.Message;
            }

            return result;
        }

    }
}
