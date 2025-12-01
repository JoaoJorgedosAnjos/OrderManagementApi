using OrderManagementApi.Models.DTOs;

namespace OrderManagementApi.Services;

public interface IOrderService
{
    Task<bool> CreateOrderAsync(OrderDto dto);
}