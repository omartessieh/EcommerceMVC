using System.ComponentModel.DataAnnotations;

namespace EcommerceMVC.Models.View
{
    public class OrderDetailsView
    {
        [Key]
        public long Id { get; set; }
        public string User_id { get; set; }
        public int Product_id { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public int Color_id { get; set; }
        public string Color { get; set; }
        public int Size_id { get; set; }
        public string Size { get; set; }
        public int Qty { get; set; }
        public int Total_Price { get; set; }
        public DateTime Created_at { get; set; }
    }
}