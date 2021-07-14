using ExperimentalityAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExperimentalityAPI.Services.Interfaces
{
    public interface IProductService
    {
        Task AddProduct(Product product);
        IEnumerable<ProductDB> Get();
    }
}
