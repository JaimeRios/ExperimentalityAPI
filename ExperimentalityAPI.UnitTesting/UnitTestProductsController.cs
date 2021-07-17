using ExperimentalityAPI.Controllers;
using ExperimentalityAPI.Models;
using ExperimentalityAPI.Models.Country;
using ExperimentalityAPI.Models.Product;
using ExperimentalityAPI.MongoDB;
using ExperimentalityAPI.Repository;
using ExperimentalityAPI.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentalityAPI.UnitTesting
{
    [TestClass]
    public class UnitTestProductsController
    {
        private MongoDbSettings mongodb;
        private MongoRepository<Product> repositoryP;
        private MongoRepository<Country> repositoryC;
        private ProductService _productService;

        public UnitTestProductsController()
        {
            mongodb = new MongoDbSettings()
            {
                ConnectionString = "mongodb+srv://AdministradorDB:qQ4l3xrDqt3nQmDS@cluster0.5uohy.mongodb.net/myFirstDatabase?retryWrites=true&w=majority",
                DatabaseName = "Experimentality"
            };

            repositoryP = new MongoRepository<Product>(mongodb);
            repositoryC = new MongoRepository<Country>(mongodb);
            _productService = new ProductService(repositoryP, repositoryC);
        }

        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            ProductController controller = new ProductController(_productService);
            
            //Act
            var response = controller.GetAll();
            
            //Assert
            Assert.AreEqual(response.Result.results.Count, 3);
        }

        [TestMethod]
        public void TestMethod_AddProduct()
        {
            //Arrange
            ProductController controller = new ProductController(_productService);
            string messageResult = "Object reference not set to an instance of an object.";

            //Act
            var response = controller.AddProduct(new ProductCreate());


            //Assert
            Assert.AreEqual(response.Result.messageResult, messageResult);

        }
    }
}
