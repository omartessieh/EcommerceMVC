using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceMVC.Models.Domain
{
    public class Review
    {
        [Key]
        public int Review_id { get; set; }

        [Required]
        public string User_id { get; set; }

        [Required]
        public int Product_id { get; set; }

        [Required]
        public int Rating { get; set; }

        [Column(TypeName = "text")]
        public string? Message { get; set; }

        public DateTime Created_at { get; set; } = DateTime.Now;
    }
}