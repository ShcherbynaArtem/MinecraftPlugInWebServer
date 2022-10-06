using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
    public class ProductDTO
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int type { get; set; }
        public double price { get; set; }
    }
}
