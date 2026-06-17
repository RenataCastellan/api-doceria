using api_doceria.Dtos;
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

    // GET api/pagamentos/pedido/1
    [HttpGet("pedido/{idPedido}")]
    public async Task<IActionResult> GetByPedido(int idPedido)
    {
        var pagamentos = await _service.GetByPedidoAsync(idPedido);
        return Ok(pagamentos);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PagamentoCreateDto dto)
    {
        try
        {
            var pagamento = await _service.CreateAsync(dto);
            if (pagamento == null)
                return BadRequest(new { mensagem = "Pedido não encontrado." });

            return Ok(pagamento);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }
}