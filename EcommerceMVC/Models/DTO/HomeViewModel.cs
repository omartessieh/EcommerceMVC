using EcommerceMVC.Models.Domain;

namespace EcommerceMVC.Models.DTO
{
    public class HomeViewModel
    {
        public List<Carousel> Carousel { get; set; }
        public List<Category> Category { get; set; }
    }
}