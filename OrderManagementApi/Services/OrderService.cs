using OrderManagementApi.Data;
using OrderManagementApi.Models.DTOs;
using OrderManagementApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace OrderManagementApi.Services;

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateOrderAsync(OrderDto dto)
    {
        try
        {
            var newOrder = new Order
            {
                OrderId = dto.numeroPedido.Replace("-01", ""),
                Value = dto.valorTotal,
                CreationDate = dto.dataCriacao.ToUniversalTime(),
                Items = new List<Item>()
            };

            foreach (var itemDto in dto.Items)
            {
                var newItem = new Item
                {
                    ProductId = int.Parse(itemDto.idItem),
                    Quantity = itemDto.quantidadeItem,
                    Price = itemDto.valorItem
                };
                newOrder.Items.Add(newItem);
            }

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Erro ao criar pedido: {e.Message}");
            return false;
        }
    }
}