using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests
{
    public class ItemsRequest<T>
        where T : notnull
    {
        public Dictionary<T, string>? Filters { get; set; }
    }
}
