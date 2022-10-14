using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
    public class CreateProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public double Price { get; set; }
    }
}
