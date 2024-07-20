using EcommerceMVC.Models.Domain;
using EcommerceMVC.Models.View;

namespace EcommerceMVC.Models.DTO
{
    public class ProductsViewModel
    {
        public List<ProductView> ProductView { get; set; }
        public List<SubCategoryView> SubCategoryView { get; set; }
        public ProductView ProductDetail { get; set; }
        public List<ProductColor> ProductColor { get; set; }
        public List<ProductSize> ProductSize { get; set; }
        public List<ProductImage> ProductImage { get; set; }
        public List<Favorite> Favorite { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }

        public int OrderQty { get; set; }
    }
}