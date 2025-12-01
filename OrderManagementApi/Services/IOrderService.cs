using System.Collections;
using OrderManagementApi.Models.DTOs;

namespace OrderManagementApi.Services;

public interface IOrderService
{
    Task<bool> CreateOrderAsync(OrderDto dto);

    Task<OrderReadDto?> GetOrderByIdAsync(string id);

    Task<IEnumerable<OrderReadDto>> GetAllOrdersAsync();
    Task<bool> UpdateOrderAsync(string id,OrderDto dto);
}