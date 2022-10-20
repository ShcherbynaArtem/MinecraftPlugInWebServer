namespace DataTransferObjects.UserItemDTOs
{
    public class GetNotReceivedItemDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ItemId { get; set; }
    }
}
