using api_doceria.Dtos;
using api_doceria.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_doceria.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarrinhosController : ControllerBase
{
    private readonly CarrinhoService _service;

    public CarrinhosController(CarrinhoService service)
    {
        _service = service;
    }

    // GET api/carrinhos/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var carrinho = await _service.GetByIdAsync(id);
        if (carrinho == null) return NotFound(new { mensagem = "Carrinho não encontrado." });
        return Ok(carrinho);
    }

    // POST api/carrinhos/cliente/1
    [HttpPost("cliente/{idCliente}")]
    public async Task<IActionResult> Create(int idCliente)
    {
        var carrinho = await _service.CreateAsync(idCliente);
        if (carrinho == null)
            return BadRequest(new { mensagem = "Cliente não encontrado." });

        return CreatedAtAction(nameof(GetById), new { id = carrinho.IdCarrinho }, carrinho);
    }

    // POST api/carrinhos/itens
    [HttpPost("itens")]
    public async Task<IActionResult> AdicionarItem([FromBody] ItemCarrinhoCreateDto dto)
    {
        var carrinho = await _service.AdicionarItemAsync(dto);
        if (carrinho == null)
            return BadRequest(new { mensagem = "Carrinho ou produto inválido." });

        return Ok(carrinho);
    }

    // DELETE api/carrinhos/itens/3
    [HttpDelete("itens/{idItem}")]
    public async Task<IActionResult> RemoverItem(int idItem)
    {
        var removido = await _service.RemoverItemAsync(idItem);
        if (!removido) return NotFound(new { mensagem = "Item não encontrado." });
        return NoContent();
    }
}