using ExperimentalityAPI.Entities;
using ExperimentalityAPI.Utils.MongoDB;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;

namespace ExperimentalityAPI.Models.Product
{
    /// <summary>
    /// Products 
    /// </summary>
    [BsonCollection("Products")]
    public class ProductCreate
    {
        [Required]
        public string name { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public double price { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Maximun percentage of discont Must be a value betwen 0-100.")]
        public int discountPercentage { get; set; }

        [Required]
        public IFormFile frontImage { get; set; }

        [Required]
        public IFormFile backImage { get; set; }

        [Required]
        public string country { get; set; }


        /// <summary>
        /// Method to convert Product on ProductCreate
        /// </summary>
        /// <param name="product"></param>
        public void fromProduct(Product product)
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
