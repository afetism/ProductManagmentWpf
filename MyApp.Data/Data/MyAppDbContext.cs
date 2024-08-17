using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

using MyApp.Data.Models.EntityModel;
using System.Data;

namespace MyApp.Data.Data;

internal class MyAppDbContext:DbContext
{
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> optionsBuilder.UseLazyLoadingProxies()
						 .UseSqlServer("Server=DESKTOP-0P1DC60\\SQLEXPRESS;Initial Catalog=AfetDb;Integrated Security=True;Encrypt=False;TrustServerCertificate=False;Connection Timeout=30;")
						  .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
			 
	


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<OrderItem>()
					.HasKey(e => new { e.OrderId, e.ProductId });
	}
	public DbSet<Category> Categories { get; set; }
	public DbSet<Product> Products { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<LikedItem> LikedItems { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<Admin> Admins { get; set; }
	public DbSet<Cart> Carts { get; set; }
	public DbSet<CreditCard> CreditCarts { get; set; }
	public DbSet<PhotoProduct> PhotoProducts { get; set; }
    public DbSet<PhotoUser> PhotoUsers { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }


}
