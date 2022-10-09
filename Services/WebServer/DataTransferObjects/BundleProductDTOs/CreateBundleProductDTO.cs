using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
    public class CreateBundleProductDTO
    {
        public Guid BundleId { get; set; }
        public Guid ProductId { get; set; }
    }
}
