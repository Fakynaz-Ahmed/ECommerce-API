using DomainLayer.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Web.Models
{
    public class Product:BaseEntity<int>
    {
        public string Name { get; set; } = null!;    
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }
        [ForeignKey("ProductBrand")]
        public int BrandId { get; set; }
        public ProductBrand ProductBrand { get; set; }

        [ForeignKey("ProductType")]
        public int TypeId { get; set; }
        public ProductType ProductType { get; set; }

    }
}