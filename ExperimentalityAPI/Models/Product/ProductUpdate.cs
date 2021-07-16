using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExperimentalityAPI.Models.Product
{
    public class ProductUpdate
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
        [Required]
        public string id { get; set; }
    }
}
