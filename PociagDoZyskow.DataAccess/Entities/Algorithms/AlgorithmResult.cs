using System;

namespace PociagDoZyskow.DataAccess.Entities.Algorithms
{
    public class AlgorithmResult
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public string ResultDescription { get; set; }

        public string AlgorithmName { get; set; }

        public bool IsBuy { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
