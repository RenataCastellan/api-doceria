using api_doceria.Dtos;
using api_doceria.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_doceria.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnderecosController : ControllerBase
{
    private readonly EnderecoService _service;

    public EnderecosController(EnderecoService service)
    {
        _service = service;
    }

    // GET api/enderecos/cliente/1
    [HttpGet("cliente/{idCliente}")]
    public async Task<IActionResult> GetByCliente(int idCliente)
    {
        var enderecos = await _service.GetByClienteAsync(idCliente);
        return Ok(enderecos);
    }

    // GET api/enderecos/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var endereco = await _service.GetByIdAsync(id);
        if (endereco == null) return NotFound(new { mensagem = "Endereço não encontrado." });
        return Ok(endereco);
    }

    // POST api/enderecos
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EnderecoCreateDto dto)
    {
        var endereco = await _service.CreateAsync(dto);
        if (endereco == null)
            return BadRequest(new { mensagem = "Cliente não encontrado." });

        return CreatedAtAction(nameof(GetById), new { id = endereco.IdEndereco }, endereco);
    }

    // DELETE api/enderecos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var removido = await _service.DeleteAsync(id);
        if (!removido) return NotFound(new { mensagem = "Endereço não encontrado." });
        return NoContent();
    }
}