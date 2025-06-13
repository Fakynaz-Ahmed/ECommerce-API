using DomainLayer.Models;

namespace E_Commerce.Web.Models
{
    public class ProductType:BaseEntity<int>
    {
        public string Name { get; set; } = default!;
    }
}
