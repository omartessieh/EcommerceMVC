using EcommerceMVC.Models.Domain;

namespace EcommerceMVC.Repositories.Abstract
{
    public interface IWebsiteRepository
    {
        Task<List<Carousel>> GetCarousels();
        Task<List<Category>> GetCategories();
    }
}