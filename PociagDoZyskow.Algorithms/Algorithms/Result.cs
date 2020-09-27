using System;
using System.Collections.Generic;
using PociagDoZyskow.DataAccess.Entities;

namespace PociagDoZyskow.Algorithms
{
    public class Result
    {
        public Company Company { get; set; }
        public string Description { get; set; }
        public string AlgorithmName { get; set; }
        public bool IsBuy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
