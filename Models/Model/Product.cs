using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Model
{
    /// <summary>
    /// Products 
    /// </summary>
    public class Product
    {
        public string name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public double discountPercentage { get; set; }
        public IFormFile frontImage { get; set; }
        public IFormFile backImage { get; set; }
        public string country { get; set; }
    }
}
