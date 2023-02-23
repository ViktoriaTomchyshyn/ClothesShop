namespace Catalog.Host.Services.Interfaces
{
    public interface IItemService
    {
        Task<int?> Add(string name, string description, string category, string brand, string size, decimal price, string pictureFileName, int availableStock);

        Task<int?> Delete(int id);

        Task<int?> Update(int id, string name, string description, string category, string brand, string size, decimal price, string pictureFileName, int availableStock);

    }
}
