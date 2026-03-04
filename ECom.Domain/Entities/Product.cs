using ECom.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECom.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    [Column("Image")]
    public string? ImageUrl { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }



    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
