using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceMVC.Models.Domain
{
    public class Stock
    {
        [Key]
        public int Stock_id { get; set; }

        [Required]
        public int Product_id { get; set; }

        [Required]
        public int Qty { get; set; }
    }
}