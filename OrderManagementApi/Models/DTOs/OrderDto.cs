namespace OrderManagementApi.Models.DTOs;

public class OrderDto
{
    public string numeroPedido { get; set; }
    public decimal valorTotal { get; set; }
    public DateTime dataCriacao { get; set; }
    public List<ItemDto> Items { get; set; } = new List<ItemDto>();
}