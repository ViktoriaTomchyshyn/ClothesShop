namespace Basket.Host.Models.Responses
{
    public class GetItemsResponse<T>
    where T : class
    {
        public IEnumerable<T> Items { get; set; } = null!;
    }
}
