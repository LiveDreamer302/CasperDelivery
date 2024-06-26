﻿namespace CasperDelivery.Data.Models;

public class Orders : BaseEntity
{
    public DateTime Date { get; set; }
    public string UserId { get; set; }
    public AppUser User { get; set; }
    public List<OrderItem> Items { get; set; }
    public decimal TotalPrice { get; set; }
}