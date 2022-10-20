namespace DataTransferObjects.BundleDTOs
{
    public class CreateBundleDTO
    {
        public string Name { get; set; }
        public double Discount { get; set; }
        public IEnumerable<Guid> ProductIds { get; set; }
    }
}
