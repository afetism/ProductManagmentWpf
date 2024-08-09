using MyApp.Data.Models.BaseEntityModel;

namespace MyApp.Data.Models.EntityModel;

public class Order:BaseEntity
{
    public DateOnly Date { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public IEnumerable<Product> Products { get; } = [];
}
