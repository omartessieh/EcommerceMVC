using System.ComponentModel.DataAnnotations;

namespace EcommerceMVC.Models.View
{
    public class SearchView
    {
        [Key]
        public long MId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
    }
}