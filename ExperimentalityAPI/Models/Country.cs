using ExperimentalityAPI.Entities;
using ExperimentalityAPI.Utils.MongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExperimentalityAPI.Models
{
    /// <summary>
    /// Model class to control maximun discount percentaje allowed
    /// </summary>
    [BsonCollection("Countries")]
    public class Country : Document
    {
        public string name { get; set; }
        public int maxDiscountPercentage { get; set; }
    }
}
