using System.Collections.Generic;

namespace PociagDoZyskow.DataAccess.Entities
{
    public class Exchange
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public ICollection<Company> Companies { get; set; }
    }
}
