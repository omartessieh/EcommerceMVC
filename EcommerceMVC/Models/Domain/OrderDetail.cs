using System.ComponentModel.DataAnnotations;

namespace EcommerceMVC.Models.Domain
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string User_id { get; set; }

        [Required]
        public int Product_id { get; set; }

        [Required]
        public int Color_id { get; set; }

        [Required]
        public int Size_id { get; set; }

        [Required]
        public int Qty { get; set; }

        [Required]
        public int Total_Price { get; set; }

        public DateTime Created_at { get; set; } = DateTime.Now;
    }
}