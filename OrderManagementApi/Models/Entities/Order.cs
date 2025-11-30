using System.ComponentModel.DataAnnotations;

namespace OrderManagementApi.Models.Entities;

public class Order
{
    [Key] public string OrderId { get; set; }
    public decimal Value { get; set; }
    public DateTime CreationDate { get; set; }
    public List<Item> Items { get; set; } = new List<Item>();
}