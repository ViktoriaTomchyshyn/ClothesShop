namespace MVC.Dtos;

public class ItemsRequest<T>
{   
    public Dictionary<T, int>? Filters { get; set; }
}