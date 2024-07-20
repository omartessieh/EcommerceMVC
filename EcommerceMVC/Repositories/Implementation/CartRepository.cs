using EcommerceMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Repositories.Implementation
{
    public class CartRepository
    {
        private readonly DatabaseContext _context;

        public CartRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<int> GetCartItemCountAsync(string userId)
        {
            var res = await _context.Cartitem.Where(c => c.User_id == userId).SumAsync(c => c.Qty);
            return res;
        }
    }
}