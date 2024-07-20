using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceMVC.Models.Domain
{
    public class SubCategory
    {
        [Key]
        public int SubCategory_id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Title { get; set; }

        [Required]
        public int Category_id { get; set; }

        public DateTime Created_at { get; set; } = DateTime.Now;
    }
}