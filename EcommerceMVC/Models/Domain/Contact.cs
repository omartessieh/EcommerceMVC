using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceMVC.Models.Domain
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string User_id { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required]
        public string Username { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required]
        public string Subject { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        [Required]
        public string Message { get; set; }

        public DateTime Created_at { get; set; } = DateTime.Now;
    }
}