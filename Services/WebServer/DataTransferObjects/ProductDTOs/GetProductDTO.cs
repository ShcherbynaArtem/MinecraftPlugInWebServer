namespace DataTransferObjects.ProductDTOs
{
    public class GetProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public double Price { get; set; }
        public bool Availability { get; set; }
    }
}
