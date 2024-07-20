using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceMVC.Models.Domain
{
    public class ProductImage
    {
        [Key]
        public int Image_id { get; set; }

        [Required]
        public int Product_id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string ImageURL { get; set; }

        [NotMapped]
        public IFormFile? ProductImageFile { get; set; }
    }
}