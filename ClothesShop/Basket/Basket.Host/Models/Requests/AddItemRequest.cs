namespace Basket.Host.Models.Requests
{
    public class AddItemRequest<T>
    where T : class
    {
        public T Item { get; set; } = null!;
    }
}
