using System;
using System.Collections.Generic;
using PociagDoZyskow.DataAccess.Entities;

namespace PociagDoZyskow.DataAccess.Entities.Algorithms
{
    public class AlgorithmResult
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public string Description { get; set; }
        public string AlgorithmName { get; set; }
        public bool IsBuy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
