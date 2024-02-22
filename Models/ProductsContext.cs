using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProductsAPI.Models;

public class ProductsContext : IdentityDbContext<AppUser, AppRole, int>
{    
    public ProductsContext(DbContextOptions<ProductsContext> options) : base(options)
    {      
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().HasData(new Product() { ProductId = 1, ProductName = "IPhone 11 Pro Max",
        Price = 1500, IsActive = true });
        modelBuilder.Entity<Product>().HasData(new Product() { ProductId = 2, ProductName = "IPhone 12 Pro Max",
        Price = 1700, IsActive = true });
        modelBuilder.Entity<Product>().HasData(new Product() { ProductId = 3, ProductName = "IPhone 13 Pro Max",
        Price = 2000, IsActive = false });
        modelBuilder.Entity<Product>().HasData(new Product() { ProductId = 4, ProductName = "IPhone 14 Pro Max",
        Price = 2500, IsActive = true });
        modelBuilder.Entity<Product>().HasData(new Product() { ProductId = 5, ProductName = "IPhone 15 Pro Max",
        Price = 3500, IsActive = true });
        
    }



    public DbSet<Product> Products { get; set; }

}