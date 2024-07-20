using System.ComponentModel.DataAnnotations;

namespace EcommerceMVC.Models.View
{
    public class ProductView
    {
        [Key]
        public long Id { get; set; }
        public int Category_id { get; set; }
        public string CategoryTitle { get; set; }
        public string CategoryImage { get; set; }
        public int SubCategory_id { get; set; }
        public string SubCategoryTitle { get; set; }
        public int Product_id { get; set; }
        public string ProductTitle { get; set; }
        public string Description { get; set; }
        public string ProductImage { get; set; }
        public int Qty { get; set; }
        public int PricePerUnit { get; set; }
        public int Discount_Percent { get; set; }
        public int PriceAfterDiscount { get; set; }
    }
}