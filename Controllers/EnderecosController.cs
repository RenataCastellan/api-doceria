using api_doceria.Dtos;
using api_doceria.Exceptions;
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

    [HttpGet("cliente/{idCliente}")]
    public async Task<IActionResult> GetByCliente(int idCliente)
    {
        try
        {
            var enderecos = await _service.GetByClienteAsync(idCliente);
            return Ok(enderecos);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var endereco = await _service.GetByIdAsync(id);
            return Ok(endereco);
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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EnderecoCreateDto dto)
    {
        try
        {
            var endereco = await _service.CreateAsync(dto);
            return Created("", endereco);
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.RemoveAsync(id);
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