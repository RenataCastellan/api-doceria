using api_doceria.Dtos;
using api_doceria.Exceptions;
using api_doceria.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_doceria.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PagamentosController : ControllerBase
{
    private readonly PagamentoService _service;

    public PagamentosController(PagamentoService service)
    {
        _service = service;
    }

    [HttpGet("pedido/{idPedido}")]
    public async Task<IActionResult> GetByPedido(int idPedido)
    {
        try
        {
            var pagamentos = await _service.GetByPedidoAsync(idPedido);
            return Ok(pagamentos);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PagamentoCreateDto dto)
    {
        try
        {
            var pagamento = await _service.CreateAsync(dto);
            return Ok(pagamento);
        }
        catch (ErrorServiceException e)
        {
            return e.ToActionResult(this);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}