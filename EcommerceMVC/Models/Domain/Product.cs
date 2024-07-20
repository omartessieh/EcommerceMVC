using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceMVC.Models.Domain
{
    public class Product
    {
        [Key]
        public int Product_id { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        [Required]
        public string Description { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string ImageURL { get; set; }

        [NotMapped]
        public IFormFile? ProductImageFile { get; set; }

        [Required]
        public int SubCategory_id { get; set; }

        [Required]
        public int Category_id { get; set; }
    }
}