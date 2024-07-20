using EcommerceMVC.Models.Domain;
using EcommerceMVC.Models.View;

namespace EcommerceMVC.Repositories.Abstract
{
    public interface IProductRepository
    {
        Task<List<ProductView>> GetProductsByCategoryAsync(int categoryId);
        Task<List<ProductView>> GetProductsBySubCategoryAsync(int subcategoryId);
        Task<List<SubCategoryView>> GetSubCategoriesByCategoryAsync(int categoryId);
        Task<ProductView> GetProductDetailsAsync(int id);
        Task<List<ProductColor>> GetProductColorsAsync(int id);
        Task<List<ProductSize>> GetProductSizesAsync(int id);
        Task<List<ProductImage>> GetProductImagesAsync(int id);
        Task<List<Favorite>> GetFavoritesAsync(string userId, int productId);
        Task<List<OrderDetail>> GetOrderDetailsAsync(int productId);
        Task<List<SearchView>> SearchAsync(string searchString);
    }
}