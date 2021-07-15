using ExperimentalityAPI.Models;
using ExperimentalityAPI.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExperimentalityAPI.Services.Interfaces
{
    public interface IProductService
    {
        Task<ResultOperationProject<Product>> AddProduct(ProductCreate product);
        IEnumerable<Product> Get();
    }
}
