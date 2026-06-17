using api_doceria.Dtos;
using api_doceria.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_doceria.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _service;

    public AuthController(AuthService service)
    {
        _service = service;
    }

    // POST api/auth/login/cliente
    [HttpPost("login/cliente")]
    public async Task<IActionResult> LoginCliente([FromBody] LoginClienteDto dto)
    {
        var resultado = await _service.LoginClienteAsync(dto);
        if (resultado == null)
            return Unauthorized(new { mensagem = "E-mail ou senha inválidos." });

        return Ok(resultado);
    }

    // POST api/auth/login/admin
    [HttpPost("login/admin")]
    public async Task<IActionResult> LoginAdmin([FromBody] LoginAdminDto dto)
    {
        var resultado = await _service.LoginAdminAsync(dto);
        if (resultado == null)
            return Unauthorized(new { mensagem = "Login ou senha inválidos." });

        return Ok(resultado);
    }
}