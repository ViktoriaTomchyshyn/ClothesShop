using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!context.Items.Any())
            {
                context.Items.AddRange(GetPreconfiguredItems());

                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<Item> GetPreconfiguredItems()
        {
            return new List<Item>()
            {
            new Item {Name = "Striped shirt", Description = "White, cotton", Size = "XS", Category = "Shirts", Brand = "Gucci", AvailableStock = 100, Price = 1550, PictureFileName = "1.png"},
            new Item {Name = "Hoody", Description = "Black, flys", Size = "XL", Category = "Hoodies", Brand = "Nike", AvailableStock = 200, Price = 2000, PictureFileName = "2.png" },
            new Item {Name = "Jeans", Description = "Blue, low waist", Size = "M", Category = "Jeans", Brand = "Levi's", AvailableStock = 150, Price = 2500, PictureFileName = "3.png" },
            new Item {Name = "T-shirt", Description = "White, short sleeve", Size = "L", Category = "T-shirts", Brand = "Adidas", AvailableStock = 250, Price = 1000, PictureFileName = "4.png" },
            new Item {Name = "Dress", Description = "Black, maxi", Size = "S", Category = "Dresses", Brand = "Zara", AvailableStock = 300, Price = 4000, PictureFileName = "5.png" },
            new Item {Name = "Jacket", Description = "Brown, leather", Size = "XL", Category = "Jackets", Brand = "Calvin Klein", AvailableStock = 250, Price = 2500, PictureFileName = "6.png" },
            new Item {Name = "Sneakers", Description = "White, high-top", Size = "9", Category = "Shoes", Brand = "Converse", AvailableStock = 50, Price = 1200, PictureFileName = "7.png"},
            new Item {Name = "Polo Shirt", Description = "Red, cotton", Size = "M", Category = "Shirts", Brand = "Ralph Lauren", AvailableStock = 75, Price = 1800, PictureFileName = "8.png"},
            new Item {Name = "Shorts", Description = "Navy, chino", Size = "32", Category = "Shorts", Brand = "Gap", AvailableStock = 100, Price = 900, PictureFileName = "9.png"},
            new Item {Name = "Blouse", Description = "Pink, silk", Size = "S", Category = "Blouses", Brand = "Equipment", AvailableStock = 60, Price = 3000, PictureFileName = "10.png"},
            new Item {Name = "Trousers", Description = "Black, straight-leg", Size = "L", Category = "Pants", Brand = "Banana Republic", AvailableStock = 125, Price = 2000, PictureFileName = "11.png"},
            };
        }
    }
}
