using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceMVC.Models.Domain
{
    public class ProductPrice
    {
        [Key]
        public int Price_id { get; set; }

        [Required]
        public int Product_id { get; set; }

        [Required]
        public int Price { get; set; }
    }
}