using ExperimentalityAPI.Models;
using ExperimentalityAPI.Models.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExperimentalityAPI.Services.Interfaces
{
    public interface ICountryService
    {
        Task<ResultOperationProject<Country>> AddCountryAsync(CountryCreate country);

        Task<ResultOperationProject<Country>> Update(CountryUpdate country);

        Task<ResultOperationProject<Country>> Delete(string id);

        Task<ResultOperationProject<Country>> Get();
    }
}
