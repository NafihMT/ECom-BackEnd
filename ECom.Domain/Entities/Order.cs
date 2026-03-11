using ECom.Domain.Common;
using ECom.Domain.Enums;
using System.Collections.Generic;

namespace ECom.Domain.Entities;

public class ShippingAddress
{
    public string F_Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
}

public class Order : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }

    public ShippingAddress ShippingAddress { get; set; } 

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}