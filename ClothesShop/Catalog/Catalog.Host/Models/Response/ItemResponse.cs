namespace Catalog.Host.Models.Response
{
    public class ItemResponse<T>
        where T : class
    {
        public T Item { get; set; } = null!;
    }
}
