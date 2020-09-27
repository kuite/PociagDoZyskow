using PociagDoZyskow.DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace PociagDoZyskow.Algorithms
{
    public class Configuration
    {
        public Company Company { get; set; }
        public string Description { get; set; }
        public bool IsBuy { get; set; }
        public DateTime DataFrom { get; set; }
    }
}
