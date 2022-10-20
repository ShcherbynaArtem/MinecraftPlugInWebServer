namespace DataTransferObjects.UserItemDTOs
{
    public class GetUserItemDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ItemId { get; set; }
        public bool Received { get; set; }
    }
}
