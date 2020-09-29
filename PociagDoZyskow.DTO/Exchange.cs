using System.Collections.Generic;

namespace PociagDoZyskow.DTO
{
    public class Exchange
    {
        public string Name { get; set; }

        public string ShortName { get; set; }

        public ICollection<Company> Companies { get; set; }
    }
}
