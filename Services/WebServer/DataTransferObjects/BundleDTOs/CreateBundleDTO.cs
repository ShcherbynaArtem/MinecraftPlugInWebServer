using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
    public class CreateBundleDTO
    {
        public string Name { get; set; }
        public double Discount { get; set; }
        public IEnumerable<Guid> ProductIds { get; set; }
    }
}
