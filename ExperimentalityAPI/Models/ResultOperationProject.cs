using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExperimentalityAPI.Models
{
    public class ResultOperationProject<T>
    {
        public bool stateOperation { get; set; }
        public string messageResult { get; set; }
        public T result { get; set; }

        public List<T> results { get; set; }
    }
}
