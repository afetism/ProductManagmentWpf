using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

using MyApp.Data.Models.EntityModel;
using System.Data;

namespace MyApp.Data.Data;

internal class MyAppDbContext:DbContext
{
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer("Server=tcp:fbmstest.database.windows.net,1433;Initial Catalog=AfetDb;Persist Security Info=False;User ID=fbmsadmin;Password=Admin12!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
			.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking) ;  

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

	

}
