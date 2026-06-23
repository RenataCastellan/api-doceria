using api_doceria.Dtos;
using api_doceria.Exceptions;
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var carrinho = await _service.GetByIdAsync(id);
            return Ok(carrinho);
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

    [HttpPost("cliente/{idCliente}")]
    public async Task<IActionResult> Create(int idCliente)
    {
        try
        {
            var carrinho = await _service.CreateAsync(idCliente);
            return Created("", carrinho);
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

    [HttpPost("itens")]
    public async Task<IActionResult> AdicionarItem([FromBody] ItemCarrinhoCreateDto dto)
    {
        try
        {
            var carrinho = await _service.AdicionarItemAsync(dto);
            return Ok(carrinho);
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

    [HttpDelete("itens/{idItem}")]
    public async Task<IActionResult> RemoverItem(int idItem)
    {
        try
        {
            await _service.RemoverItemAsync(idItem);
            return NoContent();
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