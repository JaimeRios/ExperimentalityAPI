using ExperimentalityAPI.Controllers;
using ExperimentalityAPI.Models;
using ExperimentalityAPI.Models.Country;
using ExperimentalityAPI.Models.Product;
using ExperimentalityAPI.MongoDB;
using ExperimentalityAPI.Repository;
using ExperimentalityAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

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
        public void TestMethod_AddProduct_Empty()
        {
            //Arrange
            ProductController controller = new ProductController(_productService);
            string messageResult = "Object reference not set to an instance of an object.";

            //Act
            var response = controller.AddProduct(new ProductCreate());


            //Assert
            Assert.AreEqual(response.Result.messageResult, messageResult);
        }

        [TestMethod]
        public void TestMethod_AddProduct_Complete()
        {
            //Arrange
            ProductController controller = new ProductController(_productService);
            string messageResult = "Element created successfully.";
            string path1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\ResourcesForTest", "Image1.jpeg");
            var stream1 = new FileStream(path1, FileMode.Open);
            string path2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\ResourcesForTest", "Image2.jpeg");
            var stream2 = new FileStream(path2, FileMode.Open);

            ProductCreate newProduct = new ProductCreate()
            {
                name = "Producto prueba",
                description = "Descripción de prueba",
                price = 15.99,
                discountPercentage = 0,
                country = "Colombia",
                frontImage = new FormFile(stream1, 0, stream1.Length, "ImagenFront", "ImagenFront.jpeg"),
                backImage =  new FormFile(stream2, 0, stream2.Length, "ImagenBack", "Imagenback.jpeg"),

            };

            //Act
            var responseFind = controller.GetByName("Producto prueba");
            if (responseFind.Result.result != null) 
            {
                var responseDelete = controller.Delete(responseFind.Result.result.Id.ToString());
            }
           
            var response = controller.AddProduct(newProduct);


            //Assert
            Assert.AreEqual(response.Result.messageResult, messageResult);
        }

        [TestMethod]
        public void TestMethod_DeleteProduct_NoExist()
        {
            //Arrange
            ProductController controller = new ProductController(_productService);
            string messageResult = "Does not exist a Product whit id '60f0c77c9d5dd7'.";
            string id = "60f0c77c9d5dd7";

            //Act
            var response = controller.Delete(id);


            //Assert
            Assert.AreEqual(response.Result.messageResult, messageResult);
        }


        [TestMethod]
        public void TestMethod_DeleteProduct()
        {
            //Arrange
            ProductController controller = new ProductController(_productService);
            string messageResult = "Does not exist a Product whit id '60f0c77c9d5dd7'.";
            string id = "60f0c77c9d5dd7";

            //Act
            var response = controller.Delete(id);


            //Assert
            Assert.AreEqual(response.Result.messageResult, messageResult);
        }


        [TestMethod]
        public void TestMethod_GetByName_ok()
        {
            //Arrange
            ProductController controller = new ProductController(_productService);
            string messageResult = "Element exist!!. ";
            string name = "Camisa Deportiva";

            //Act
            var response = controller.GetByName(name);


            //Assert
            Assert.AreEqual(response.Result.messageResult, messageResult);
        }

        [TestMethod]
        public void TestMethod_GetByName_Nofound()
        {
            //Arrange
            ProductController controller = new ProductController(_productService);
            string messageResult = "Does not exist a Product whit name 'Un producto que no existe'.";
            string name = "Un producto que no existe";

            //Act
            var response = controller.GetByName(name);


            //Assert
            Assert.AreEqual(response.Result.messageResult, messageResult);
        }

        [TestMethod]
        public void TestMethod_GetMoreSearched_TotalisMinus()
        {
            //Arrange
            ProductController controller = new ProductController(_productService);
            string messageResult = "There are a total 3 of Products registered.";
            int total = 5;

            //Act
            var response = controller.GetMoreSearched(total);


            //Assert
            Assert.AreEqual(response.Result.messageResult, messageResult);
        }

        [TestMethod]
        public void TestMethod_GetMoreSearched_TotalOk()
        {
            //Arrange
            ProductController controller = new ProductController(_productService);
            string messageResult = "The 2 most consulted product by the name.";
            int total = 2;

            //Act
            var response = controller.GetMoreSearched(total);


            //Assert
            Assert.AreEqual(response.Result.messageResult, messageResult);
        }

    }
}
