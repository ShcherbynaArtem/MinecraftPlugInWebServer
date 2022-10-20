using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class BundleProductEntity
    {
        [Column("bundle_id")]
        public Guid BundleId { get; set; }
        [Column("product_id")]
        public Guid ProductId { get; set; }

        public BundleProductEntity(Guid bundleId, Guid productId)
        {
            this.BundleId = bundleId;
            this.ProductId = productId;
        }   
    }
}
