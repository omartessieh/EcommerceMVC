using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceMVC.Models.View
{
    public class CartItemView
    {
        [Key]
        public long Id { get; set; }
        public string User_id { get; set; }
        public int Cartitem_id { get; set; }
        public int Product_id { get; set; }
        public int Color_id { get; set; }
        public int Size_id { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int StockQty { get; set; }
        public string CategoryTitle { get; set; }
        public string SubCategoryTitle { get; set; }
        public string ProductTitle { get; set; }
        public string ProductImage { get; set; }
        public int PricePerUnit { get; set; }
        public int PriceAfterDiscount { get; set; }
        public int Discount_Percent { get; set; }
        public int OrderQty { get; set; }
        public int OrderPrice { get; set; }
    }
}