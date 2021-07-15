using ExperimentalityAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExperimentalityAPI.Services.Interfaces
{
    public interface ICountryService
    {
        Task<ResultOperationProject<Country>> AddCountryAsync(Country country);

        Task Update(Country country);

        Task Delete(string id);

        List<Country> Get();
    }
}
