using System.ComponentModel.DataAnnotations;

namespace EcommerceMVC.Models.View
{
    public class FavoriteView
    {
        [Key]
        public long Id { get; set; }
        public string User_id { get; set; }
        public int Product_id { get; set; }
        public string CategoryTitle { get; set; }
        public string SubCategoryTitle { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public DateTime Created_at { get; set; }
    }
}