using ExperimentalityAPI.Entities;
using ExperimentalityAPI.Utils.MongoDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExperimentalityAPI.Models.Product
{
    [BsonCollection("Products")]
    public class Product : Document
    {
        public string name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public int discountPercentage { get; set; }
        public string frontImageData { get; set; }
        public string frontImageExtension { get; set; }
        public string backImageData { get; set; }
        public string backImageExtension { get; set; }
        public string country { get; set; }
        public string consulted { get; set; }

        public void fromProductCreate(ProductCreate product)
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
