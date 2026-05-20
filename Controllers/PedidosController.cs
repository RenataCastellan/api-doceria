using api_doceria.Dtos;
using api_doceria.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_doceria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly PedidoService _service;

        public PedidosController(PedidoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pedidos = await _service.GetAllAsync();
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pedido = await _service.GetByIdAsync(id);
            if (pedido == null) return NotFound(new { mensagem = "Pedido não encontrado." });
            return Ok(pedido);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PedidoCreateDto dto)
        {
            var pedido = await _service.CreateAsync(dto);
            if (pedido == null)
                return BadRequest(new { mensagem = "Cliente, endereço ou produtos inválidos." });

            return CreatedAtAction(nameof(GetById), new { id = pedido.IdPedido }, pedido);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> AtualizarStatus(int id, [FromBody] string status)
        {
            var atualizado = await _service.AtualizarStatusAsync(id, status);
            if (!atualizado) return NotFound(new { mensagem = "Pedido não encontrado." });
            return NoContent();
        }
    }
}
