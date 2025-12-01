using Microsoft.AspNetCore.Mvc;
using OrderManagementApi.Models.DTOs;
using OrderManagementApi.Services;

namespace OrderManagementApi.Controllers;

[ApiController]
[Route("order")]
public class OrderConttroler : ControllerBase
{
    private readonly IOrderService _service;

    public OrderConttroler(IOrderService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OrderDto dto)
    {
        try
        {
            var sucess = await _service.CreateOrderAsync(dto);
            if (sucess)
            {
                return StatusCode(201, new { message = "Pedido criado com sucesso!" });
            }
            else
            {
                return BadRequest("Não foi possível processar o pedido");
            }
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Erro interno: {e.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetOrderById([FromRoute] string id)
    {
        var orderDto = await _service.GetOrderByIdAsync(id);

        if (orderDto == null)
        {
            return NotFound(new { message = "Pedido não encontrado." });
        }

        return Ok(orderDto);
    }

    [HttpGet("list")]
    public async Task<ActionResult<List<OrderReadDto>>> GetAllOrders()
    {
        var orders = await _service.GetAllOrdersAsync();

        return Ok(orders);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderById(string id, [FromBody] OrderDto dto)
    {
        if (id != dto.numeroPedido) return BadRequest("Pedido não encontrado.");

        var updated = await _service.UpdateOrderAsync(id, dto);

        if (!updated) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderById(string? id)
    {
        if (id == null) return BadRequest("Pedido não encontrado.");
        var deleted = await _service.DeleteOrderAsync(id);
        
        if (!deleted) return NotFound();
        return NoContent();
    }
}