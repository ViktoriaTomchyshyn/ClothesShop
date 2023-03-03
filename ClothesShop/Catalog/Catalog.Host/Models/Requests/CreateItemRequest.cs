using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests
{
    public class CreateItemRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [Required]
        [StringLength(30)]
        public string Category { get; set; }

        [StringLength(30)]
        public string Brand { get; set; }

        [Required]
        [StringLength(10)]
        public string Size { get; set; }

        [Required]
        [Range(0, 99999.99)]
        public decimal Price { get; set; }

        public string PictureFileName { get; set; }

        [Required]
        public int AvailableStock { get; set; }
    }
}
