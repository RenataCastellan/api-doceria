using api_doceria.Dtos;
using api_doceria.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_doceria.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdministradoresController : ControllerBase
{
    private readonly AdministradorService _service;

    public AdministradoresController(AdministradorService service)
    {
        _service = service;
    }

    // GET api/administradores
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var admins = await _service.GetAllAsync();
        return Ok(admins);
    }

    // GET api/administradores/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var admin = await _service.GetByIdAsync(id);
        if (admin == null) return NotFound(new { mensagem = "Administrador não encontrado." });
        return Ok(admin);
    }

    // POST api/administradores
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AdministradorCreateDto dto)
    {
        try
        {
            var admin = await _service.CreateAsync(dto);
            if (admin == null)
                return Conflict(new { mensagem = "Login já cadastrado no sistema." });

            return CreatedAtAction(nameof(GetById), new { id = admin.IdAdmin }, admin);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }
}