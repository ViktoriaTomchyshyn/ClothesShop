namespace MVC.Models.Requests
{
    public class DeleteItemsRequest
    {
        [Required]
        public int ItemId { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
