using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BundleProductEntity
    {
        public Guid bundle_id { get; set; }
        public Guid product_id { get; set; }

        public BundleProductEntity(Guid bundle_id, Guid product_id)
        {
            this.bundle_id = bundle_id;
            this.product_id = product_id;
        }   
    }
}
