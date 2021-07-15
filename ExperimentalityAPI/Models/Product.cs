using ExperimentalityAPI.Entities;
using ExperimentalityAPI.Utils.MongoDB;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExperimentalityAPI.Models
{
    /// <summary>
    /// Products 
    /// </summary>
    [BsonCollection("Products")]
    public class Product
    {
        public string name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public int discountPercentage { get; set; }
        public IFormFile frontImage { get; set; }
        public IFormFile backImage { get; set; }
        public string country { get; set; }

        public void fromProduct(ProductDB product)
        {
            this.name = product.name;
            this.description = product.description;
            this.price = product.price;
            this.discountPercentage = product.discountPercentage;
            this.country = product.country;

            var bytes = Convert.FromBase64String(product.frontImageData);
            var ms = new MemoryStream(bytes);
            this.frontImage =  new FormFile(ms, 0, ms.Length,$"frontImage.{product.frontImageExtension}","frontImage");

            var bytes2 = Convert.FromBase64String(product.backImageData);
            var ms2 = new MemoryStream(bytes2);
            this.frontImage = new FormFile(ms2, 0, ms2.Length, $"backImage.{product.backImageExtension}", "backImage");
        }
    }
}
