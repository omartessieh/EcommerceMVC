using EcommerceMVC.Data;
using EcommerceMVC.Models.Domain;
using EcommerceMVC.Models.View;
using EcommerceMVC.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Repositories.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly DatabaseContext _context;

        public ProductRepository(DatabaseContext context)
        {
            this._context = context;
        }

        public async Task<List<ProductView>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _context.ProductViews.Where(p => p.Category_id == categoryId).ToListAsync();
        }

        public async Task<List<ProductView>> GetProductsBySubCategoryAsync(int subcategoryId)
        {
            return await _context.ProductViews.Where(p => p.SubCategory_id == subcategoryId).ToListAsync();
        }

        public async Task<List<SubCategoryView>> GetSubCategoriesByCategoryAsync(int categoryId)
        {
            return await _context.SubCategoryViews.Where(p => p.Category_id == categoryId).ToListAsync();
        }

        public async Task<ProductView> GetProductDetailsAsync(int id)
        {
            return await _context.ProductViews.FirstOrDefaultAsync(p => p.Product_id == id);
        }

        public async Task<List<ProductColor>> GetProductColorsAsync(int id)
        {
            return await _context.ProductColor.Where(p => p.Product_id == id).ToListAsync();
        }

        public async Task<List<ProductSize>> GetProductSizesAsync(int id)
        {
            return await _context.ProductSize.Where(p => p.Product_id == id).ToListAsync();
        }

        public async Task<List<ProductImage>> GetProductImagesAsync(int id)
        {
            return await _context.ProductImage.Where(p => p.Product_id == id).ToListAsync();
        }

        public async Task<List<Favorite>> GetFavoritesAsync(string userId, int productId)
        {
            return await _context.Favorite.Where(p => p.User_id == userId && p.Product_id == productId).ToListAsync();
        }

        public async Task<List<OrderDetail>> GetOrderDetailsAsync(int productId)
        {
            return await _context.OrderDetail.Where(p => p.Product_id == productId).ToListAsync();
        }

        public async Task<List<SearchView>> SearchAsync(string searchString)
        {
            return await _context.SearchViews.Where(e => e.Title.Contains(searchString)).ToListAsync();
        }
    }
}