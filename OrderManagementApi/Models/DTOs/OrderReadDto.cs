namespace OrderManagementApi.Models.DTOs;

public class OrderReadDto
{
    public string OrderId { get; set; }
    
    public decimal Value { get; set; }
    
    public DateTime CreationDate { get; set; }
    
    public List<ItemReadDto> Items { get; set; } = new List<ItemReadDto>();
}