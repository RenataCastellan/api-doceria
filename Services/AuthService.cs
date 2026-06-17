using api_doceria.DataContexts;
using api_doceria.Dtos;
using Microsoft.EntityFrameworkCore;

namespace api_doceria.Services;

public class AuthService
{
    private readonly AppDbContext _context;
    private readonly TokenService _tokenService;

    public AuthService(AppDbContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public async Task<LoginResponseDto?> LoginClienteAsync(LoginClienteDto dto)
    {
        var cliente = await _context.Clientes
            .FirstOrDefaultAsync(c => c.Email == dto.Email);

        if (cliente == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, cliente.Senha))
            return null;

        var token = _tokenService.GerarTokenCliente(cliente.IdCliente, cliente.Nome);

        return new LoginResponseDto
        {
            Id = cliente.IdCliente,
            Nome = cliente.Nome,
            Perfil = "Cliente",
            Token = token
        };
    }

    public async Task<LoginResponseDto?> LoginAdminAsync(LoginAdminDto dto)
    {
        var admin = await _context.Administradores
            .FirstOrDefaultAsync(a => a.Login == dto.Login);

        if (admin == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, admin.Senha))
            return null;

        var token = _tokenService.GerarTokenAdmin(admin.IdAdmin, admin.Login);

        return new LoginResponseDto
        {
            Id = admin.IdAdmin,
            Nome = admin.Login,
            Perfil = "Administrador",
            Token = token
        };
    }
}