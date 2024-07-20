using EcommerceMVC.Models.Domain;
using EcommerceMVC.Models.DTO;
using EcommerceMVC.Models.View;

namespace EcommerceMVC.Repositories.Abstract
{
    public interface IUserRepository
    {
        Task<List<FavoriteView>> GetFavoriteAsync(string userId);
        Task<List<CartItemView>> GetCartItemAsync(string userId);
        Task<List<OrderDetailsView>> GetOrderDetailsAsync(string userId);

        Task<Cartitem> AddToCartAsync(Cartitem cartitem, string userId);
        Task<Favorite> AddFavoriteAsync(int productId, string userId);
        Task RemoveFavoriteAsync(int productId, string userId);
        Task RemoveItem(string userId, int product_id, int cartitem_id);
        Task UpdateQtyCartItem(Cartitem cartitem);
        Task Checkout_Click(string UserId);
        Task<int> GetCartItemCountAsync(string userId);
    }
}