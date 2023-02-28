using System.ComponentModel.DataAnnotations;

namespace Basket.Host.Models.Requests
{
    public class DeleteItemRequest
    {
        [Required]
        public int ItemId { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
