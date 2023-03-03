namespace MVC.Models.Responses
{
    public class ItemResponse<T>
        where T : class
    {
        public T Item { get; set; } = null!;
    }
}
