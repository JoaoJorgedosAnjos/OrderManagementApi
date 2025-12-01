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
}