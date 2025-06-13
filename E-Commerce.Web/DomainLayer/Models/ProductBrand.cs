using DomainLayer.Models;

namespace E_Commerce.Web.Models
{
    public class ProductBrand:BaseEntity<int>
    {
        public string Name { get; set; } = default!;
        //public ICollection<Product> Products { get; set; }= new HashSet<Product>();
    }
}
