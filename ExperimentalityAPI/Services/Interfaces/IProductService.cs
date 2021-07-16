using ExperimentalityAPI.Models;
using ExperimentalityAPI.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExperimentalityAPI.Services.Interfaces
{
    /// <summary>
    /// Interface for product service
    /// </summary>
    public interface IProductService
    {
        Task<ResultOperationProject<Product>> AddProduct(ProductCreate product);

        Task<ResultOperationProject<Product>> Update(ProductUpdate product);

        Task<ResultOperationProject<Product>> Delete(string id);

        Task<ResultOperationProject<Product>> Get();

        Task<ResultOperationProject<Product>> GetByName(string name);

        Task<ResultOperationProject<Product>> GetMoreSearched(int count);

    }
}
