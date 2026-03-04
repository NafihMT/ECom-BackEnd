using ECom.Domain.Common;
using ECom.Domain.Enums;
using System.Collections.Generic;

namespace ECom.Domain.Entities;

public class Order : BaseEntity
{

    public int UserId { get; set; }  // Foriegn Key
    public User User { get; set; }

    public OrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}