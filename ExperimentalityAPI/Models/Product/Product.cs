using ExperimentalityAPI.Entities;
using ExperimentalityAPI.Utils.MongoDB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExperimentalityAPI.Models.Product
{
    [BsonCollection("Products")]
    public class Product : Document
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
        public string frontImageData { get; set; }

        [Required]
        public string frontImageExtension { get; set; }

        [Required]
        public string backImageData { get; set; }

        [Required]
        public string backImageExtension { get; set; }

        [Required]
        public string country { get; set; }

        [Required]
        public ulong consulted { get; set; }

        /// <summary>
        /// Method to convert ProductCreate on Product
        /// </summary>
        /// <param name="product"></param>
        public void fromProductCreate(ProductCreate product)
        {
            this.name = product.name;
            this.description = product.description;
            this.price = product.price;
            this.discountPercentage = product.discountPercentage;
            this.country = product.country;
            this.consulted = 0;

            var ms = new MemoryStream();
            product.frontImage.CopyTo(ms);
            var fileBytes = ms.ToArray();
            string fileString = Convert.ToBase64String(fileBytes);
            this.frontImageData = fileString;
            this.frontImageExtension = product.frontImage.FileName.Split('.').ElementAt(1);

            var ms2 = new MemoryStream();
            product.backImage.CopyTo(ms2);
            fileBytes = ms2.ToArray();
            fileString = Convert.ToBase64String(fileBytes);
            this.backImageData = fileString;
            this.backImageExtension = product.backImage.FileName.Split('.').ElementAt(1);

        }

        public void fromProductUpdate(ProductUpdate product)
        {
            this.name = product.name;
            this.description = product.description;
            this.price = product.price;
            this.discountPercentage = product.discountPercentage;
            this.country = product.country;

            var ms = new MemoryStream();
            product.frontImage.CopyTo(ms);
            var fileBytes = ms.ToArray();
            string fileString = Convert.ToBase64String(fileBytes);
            this.frontImageData = fileString;
            this.frontImageExtension = product.frontImage.FileName.Split('.').ElementAt(1);

            var ms2 = new MemoryStream();
            product.backImage.CopyTo(ms2);
            fileBytes = ms2.ToArray();
            fileString = Convert.ToBase64String(fileBytes);
            this.backImageData = fileString;
            this.backImageExtension = product.backImage.FileName.Split('.').ElementAt(1);

        }
    }
}
