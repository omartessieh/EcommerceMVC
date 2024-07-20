using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceMVC.Models.Domain
{
    public class ProductColor
    {
        [Key]
        public int Color_id { get; set; }

        [Required]
        public int Product_id { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required]
        public string Color { get; set; }
    }
}