using EcommerceMVC.Data;
using EcommerceMVC.Models.Domain;
using EcommerceMVC.Models.View;
using EcommerceMVC.Repositories.Abstract;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EcommerceMVC.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<FavoriteView>> GetFavoriteAsync(string userId)
        {
            return await _context.FavoriteViews.Where(p => p.User_id == userId).ToListAsync();
        }

        public async Task<List<CartItemView>> GetCartItemAsync(string userId)
        {
            return await _context.CartItemViews.Where(p => p.User_id == userId).ToListAsync();
        }

        public async Task<List<OrderDetailsView>> GetOrderDetailsAsync(string userId)
        {
            return await _context.OrderDetailsViews.Where(p => p.User_id == userId).ToListAsync();
        }


        public async Task<Cartitem> AddToCartAsync(Cartitem cartitem, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ApplicationException("User is not authenticated.");
            }

            var check = await _context.Cartitem
                .Where(p => p.Product_id == cartitem.Product_id &&
                            p.Color_id == cartitem.Color_id &&
                            p.Size_id == cartitem.Size_id &&
                            p.User_id == userId)
                .FirstOrDefaultAsync();

            if (check != null)
            {
                throw new ApplicationException("Product Already in Cart.");
            }

            cartitem.User_id = userId;
            _context.Cartitem.Add(cartitem);
            await _context.SaveChangesAsync();

            return cartitem;
        }

        public async Task<Favorite> AddFavoriteAsync(int productId, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ApplicationException("User is not authenticated.");
            }

            var existingFavorite = await _context.Favorite
                .FirstOrDefaultAsync(m => m.User_id == userId && m.Product_id == productId);

            if (existingFavorite != null)
            {
                throw new ApplicationException("Product is already a favorite.");
            }

            var favorite = new Favorite
            {
                Product_id = productId,
                User_id = userId
            };

            _context.Favorite.Add(favorite);
            await _context.SaveChangesAsync();

            return favorite;
        }

        public async Task RemoveFavoriteAsync(int productId, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ApplicationException("User is not authenticated.");
            }

            var favorite = await _context.Favorite.FirstOrDefaultAsync(m => m.User_id == userId && m.Product_id == productId);

            if (favorite != null)
            {
                _context.Favorite.Remove(favorite);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveItem(string userId, int product_id, int cartitem_id)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ApplicationException("User is not authenticated.");
            }

            var cartitem = await _context.Cartitem.FirstOrDefaultAsync(m => m.User_id == userId && m.Product_id == product_id && m.Cartitem_id == cartitem_id);

            if (cartitem != null)
            {
                _context.Cartitem.Remove(cartitem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateQtyCartItem(Cartitem cartitem)
        {
            _context.Cartitem.Update(cartitem);
            await _context.SaveChangesAsync();
        }

        public async Task Checkout_Click(string UserId)
        {
            var cartItems = await _context.CartItemViews.Where(p => p.User_id == UserId).ToListAsync();

            if (cartItems == null || cartItems.Count == 0)
            {
                throw new ApplicationException("No Item.");
            }

            var orderDetails = new List<OrderDetail>();

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    User_id = UserId,
                    Product_id = item.Product_id,
                    Color_id = item.Color_id,
                    Size_id = item.Size_id,
                    Qty = item.OrderQty,
                    Total_Price = item.OrderPrice
                };
                _context.OrderDetail.Add(orderDetail);
            }

            var cartItemsToRemove = await _context.Cartitem.Where(ci => ci.User_id == UserId).ToListAsync();
            _context.Cartitem.RemoveRange(cartItemsToRemove);

            await _context.SaveChangesAsync();
        }

        public async Task<int> GetCartItemCountAsync(string userId)
        {
            var res = await _context.Cartitem.Where(c => c.User_id == userId).SumAsync(c => c.Qty);
            return res;
        }
    }
}