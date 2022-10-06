using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
    public class BundleDTO
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public double discount { get; set; }
        public IEnumerable<Guid> product_ids { get; set; }
    }
}
