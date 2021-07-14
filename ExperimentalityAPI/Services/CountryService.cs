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


        public async Task AddCountry(Country country)
        {
            try
            {
                await _repository.InsertOneAsync(country);
            }
            catch (Exception exc)
            {
                var message = exc.Message;
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
