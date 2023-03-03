namespace Catalog.Host.Models.Dtos
{
    public class ItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string Brand { get; set; }

        public string Size { get; set; }

        public decimal Price { get; set; }

        public string PictureUrl { get; set; }

        public int AvailableStock { get; set; }
    }
}
