namespace Catalog.Host.Models.Requests
{
    public class CreateItemRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string Brand { get; set; }

        public string Size { get; set; }

        public decimal Price { get; set; }

        public string PictureFileName { get; set; }

        public int AvailableStock { get; set; }

    }
}
