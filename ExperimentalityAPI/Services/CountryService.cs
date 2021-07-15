using ExperimentalityAPI.Models;
using ExperimentalityAPI.Repository.Interfaces;
using ExperimentalityAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExperimentalityAPI.Services
{
    public class CountryService: ICountryService
    {
        private readonly IMongoRepository<Country> _repository;

        public CountryService(IMongoRepository<Country> repository)
        {
            _repository = repository;
        }


        public async Task<ResultOperationProject<Country>> AddCountryAsync(Country country)
        {
            ResultOperationProject<Country> result = new ResultOperationProject<Country>();
            result.result = null;
            try
            {
                if(_repository.FilterBy(c => c.name == country.name).FirstOrDefault() == null)
                {
                    await _repository.InsertOneAsync(country);
                    result.result = _repository.FilterBy(c => c.name == country.name).FirstOrDefault();
                    result.messageResult = "Element created successfully";
                    result.stateOperation = true;
                }
                else
                {
                    result.messageResult = $"Already exist a country named {country.name}";
                    result.stateOperation = true;
                }
                
                return result;
            }
            catch (Exception exc)
            {
                result.stateOperation = true;
                result.messageResult = exc.Message;
                return result;
            }
        }

        public async Task Update(Country country)
        {
            try
            {
                var countryForUpdate = _repository.FilterBy(c => c.name == country.name).FirstOrDefault();
                countryForUpdate.maxDiscountPercentage = country.maxDiscountPercentage;
                await _repository.ReplaceOneAsync(countryForUpdate);
            }
            catch (Exception exc)
            {

                var message = exc.Message;
            }

        }

        public async Task Delete(string id)
        {
            try
            {
                _repository.DeleteById(id);
            }
            catch (Exception exc)
            {

                var message = exc.Message;
            }

        }

        public List<Country> Get()
        {
            List<Country> countries = new List<Country>();
            try
            {
                countries = _repository.AsQueryable().ToList();

            }
            catch (Exception exc)
            {
                var message = exc.Message;
            }

            return countries;
        }

    }
}
