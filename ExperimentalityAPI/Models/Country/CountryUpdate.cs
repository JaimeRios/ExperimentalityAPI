using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExperimentalityAPI.Models.Country
{
    public class CountryUpdate
    {
        [Required]
        public string name { get; set; }
        [Required]
        [Range(1, 100, ErrorMessage = "Maximun percentage of discont Must be a value betwen 0-100.")]
        public int maxDiscountPercentage { get; set; }
        [Required]
        public string id { get; set; }
    }
}
