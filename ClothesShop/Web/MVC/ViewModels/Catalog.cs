using Newtonsoft.Json;

namespace MVC.ViewModels;

public record Catalog
{
    [JsonProperty]
    public List<Item> Data { get; init; }
}
