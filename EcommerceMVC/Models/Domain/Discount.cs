using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceMVC.Models.Domain
{
    public class Discount
    {
        [Key]
        public int Discount_id { get; set; }

        [Required]
        public int Product_id { get; set; }

        [Required]
        public int Discount_Percent { get; set; }
    }
}