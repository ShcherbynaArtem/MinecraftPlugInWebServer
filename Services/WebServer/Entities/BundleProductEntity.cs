using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BundleProductEntity
    {
        public Guid BundleId { get; set; }
        public Guid ProductId { get; set; }

        public BundleProductEntity(Guid bundle_id, Guid product_id)
        {
            this.BundleId = bundle_id;
            this.ProductId = product_id;
        }   
    }
}
