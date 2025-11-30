namespace OrderManagementApi.Models.Entities;

public class Item
{
    public string OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public Order Order { get; set; }
}