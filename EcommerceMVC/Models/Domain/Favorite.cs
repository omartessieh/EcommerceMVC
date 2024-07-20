using System.ComponentModel.DataAnnotations;

namespace EcommerceMVC.Models.Domain
{
    public class Favorite
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string User_id { get; set; }

        [Required]
        public int Product_id { get; set; }

        public DateTime Created_at { get; set; } = DateTime.Now;
    }
}