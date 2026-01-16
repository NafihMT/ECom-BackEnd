using ECom.Domain.Common;

namespace ECom.Domain.Entities;

public class OrderItem : BaseEntity
{
    public int OrderId { get; set; } // Foriegn Key
    public Order Order { get; set; }

    public int ProductId { get; set; }   // Foriegn Key
    public Product Product { get; set; }

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}