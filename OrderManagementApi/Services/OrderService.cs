using System.Collections;
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

    public async Task<OrderReadDto?> GetOrderByIdAsync(string id)
    {
        try
        {
            var orderEntity = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (orderEntity == null) return null;

            return MapToDto(orderEntity);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Erro ao buscar pedido: {e.Message}");
            return null;
        }
    }

    public async Task<IEnumerable<OrderReadDto>> GetAllOrdersAsync()
    {
        var orderEntities = await _context.Orders
            .Include(o => o.Items)
            .ToListAsync();

        if (orderEntities.Count == 0) return new List<OrderReadDto>();

        return orderEntities
            .Select(o => MapToDto(o))
            .ToList();
    }

    private OrderReadDto MapToDto(Order orderEntity)
    {
        return new OrderReadDto
        {
            OrderId = orderEntity.OrderId,
            Value = orderEntity.Value,
            CreationDate = orderEntity.CreationDate,

            Items = orderEntity.Items.Select(i => new ItemReadDto
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList()
        };
    }
}