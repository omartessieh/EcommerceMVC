using EcommerceMVC.Models;
using EcommerceMVC.Models.View;

namespace EcommerceMVC.Models.DTO
{
    public class CartViewModel
    {
        public List<CartItemView> CartItems { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal Shipping { get; set; }
        public decimal CheckTotalQuantity { get; set; }
        public decimal Total { get; set; }
    }
}