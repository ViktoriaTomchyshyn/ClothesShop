namespace MVC.Dtos;

public class ItemsRequest<T>
{   
    public Dictionary<T, string>? Filters { get; set; }
}