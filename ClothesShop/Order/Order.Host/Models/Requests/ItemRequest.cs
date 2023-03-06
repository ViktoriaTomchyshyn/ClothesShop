namespace Order.Models.Requests
{
    public class ItemRequest<T>
     where T : class
    {
        public T Item { get; set; } = null!;
    }
}
