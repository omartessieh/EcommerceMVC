using EcommerceMVC.Models.Domain;
using EcommerceMVC.Models.View;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Data
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductView>().ToTable("ProductView");
            modelBuilder.Entity<SubCategoryView>().ToTable("SubCategoryView");
            modelBuilder.Entity<SearchView>().ToTable("SearchView");
            modelBuilder.Entity<FavoriteView>().ToTable("FavoriteView");
            modelBuilder.Entity<CartItemView>().ToTable("CartItemView");
            modelBuilder.Entity<OrderDetailsView>().ToTable("OrderDetailsView");
        }

        public DbSet<Carousel> Carousel { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Cartitem> Cartitem { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Discount> Discount { get; set; }
        public DbSet<Favorite> Favorite { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<ProductColor> ProductColor { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }
        public DbSet<ProductPrice> ProductPrice { get; set; }
        public DbSet<ProductSize> ProductSize { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Stock> Stock { get; set; }

        public DbSet<ProductView> ProductViews { get; set; }
        public DbSet<SubCategoryView> SubCategoryViews { get; set; }
        public DbSet<SearchView> SearchViews { get; set; }
        public DbSet<FavoriteView> FavoriteViews { get; set; }
        public DbSet<CartItemView> CartItemViews { get; set; }
        public DbSet<OrderDetailsView> OrderDetailsViews { get; set; }
    }
}