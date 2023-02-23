namespace Catalog.Host.Data.Entities
{
    public class Item
    {
         public int Id { get; set; }

         public string Name { get; set; } = null!;

         public string Description { get; set; } = null!;

         public string Category { get; set; } = null!;

         public string Brand { get; set; }

         public string Size { get; set; } = null!;

         public decimal Price { get; set; }

         public string PictureFileName { get; set; } = null!;

         public int AvailableStock { get; set; }
    }
}
