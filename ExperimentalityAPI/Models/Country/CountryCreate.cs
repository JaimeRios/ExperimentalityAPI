using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExperimentalityAPI.Models.Country
{
    public class CountryCreate
    {
        [Required]
        public string name { get; set; }
        [Required]
        public int maxDiscountPercentage { get; set; }
    }
}
