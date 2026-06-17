using api_doceria.Dtos;
using api_doceria.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_doceria.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly ProdutoService _service;

    public ProdutosController(ProdutoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? categoria)
    {
        var produtos = await _service.GetAllAsync(categoria);
        return Ok(produtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var produto = await _service.GetByIdAsync(id);
        if (produto == null) return NotFound(new { mensagem = "Produto não encontrado." });
        return Ok(produto);
    }

    [Authorize(Roles = "Administrador")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProdutoCreateDto dto)
    {
        var produto = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = produto.IdProduto }, produto);
    }

    [Authorize(Roles = "Administrador")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProdutoUpdateDto dto)
    {
        var atualizado = await _service.UpdateAsync(id, dto);
        if (!atualizado) return NotFound(new { mensagem = "Produto não encontrado." });
        return NoContent();
    }

    [Authorize(Roles = "Administrador")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var removido = await _service.DeleteAsync(id);
        if (!removido) return NotFound(new { mensagem = "Produto não encontrado." });
        return NoContent();
    }
}