using MyApp.Data.Models.BaseEntityModel;

namespace MyApp.Data.Models.EntityModel;

public class User:BaseEntity
{
    public string Firstname { get; set; } = null!;
    public string LatsName { get; set; } = null!;
    public DateOnly DateofBirth { get; set; }
    public string Email { get; set; } = null!;

    public virtual IEnumerable<Order>? History { get; } = [];
    public byte[] Password { get; set; } = null!;
   
	public virtual IEnumerable<CreditCard>? CreditCards { get; } = [];

}
