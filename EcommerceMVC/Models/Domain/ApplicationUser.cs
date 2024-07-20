using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceMVC.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Country { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public int Floor { get; set; }
        public DateTime Created_at { get; set; } = DateTime.Now;

        [NotMapped]
        public string Role { get; set; }
    }
}