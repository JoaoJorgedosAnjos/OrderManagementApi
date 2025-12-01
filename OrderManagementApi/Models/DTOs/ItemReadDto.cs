namespace OrderManagementApi.Models.DTOs;

public class ItemReadDto
{
    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }
}