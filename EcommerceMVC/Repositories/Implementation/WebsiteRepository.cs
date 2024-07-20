using EcommerceMVC.Data;
using EcommerceMVC.Models.Domain;
using EcommerceMVC.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Repositories.Implementation
{
    public class WebsiteRepository : IWebsiteRepository
    {
        private readonly DatabaseContext databaseContext;

        public WebsiteRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<List<Carousel>> GetCarousels()
        {
            var carousels = await databaseContext.Carousel.ToListAsync();
            return carousels;
        }

        public async Task<List<Category>> GetCategories()
        {
            var Categories = await databaseContext.Category.ToListAsync();
            return Categories;
        }
    }
}