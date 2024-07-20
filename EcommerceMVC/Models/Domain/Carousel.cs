using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceMVC.Models.Domain
{
    public class Carousel
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required]
        public string Title { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string ImageURL { get; set; }

        [NotMapped]
        public IFormFile? CarouselImageFile { get; set; }

        public DateTime Created_at { get; set; } = DateTime.Now;
    }
}