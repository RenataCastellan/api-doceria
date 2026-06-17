using api_doceria.DataContexts;
using api_doceria.Dtos;
using api_doceria.Models;
using Microsoft.EntityFrameworkCore;

namespace api_doceria.Services;

public class AdministradorService
{
    private readonly AppDbContext _context;

    public AdministradorService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<AdministradorReadDto>> GetAllAsync()
    {
        return await _context.Administradores.Select(a => new AdministradorReadDto
        {
            IdAdmin = a.IdAdmin,
            Login = a.Login
        }).ToListAsync();
    }

    public async Task<AdministradorReadDto?> GetByIdAsync(int id)
    {
        var admin = await _context.Administradores.FindAsync(id);
        if (admin == null) return null;

        return new AdministradorReadDto
        {
            IdAdmin = admin.IdAdmin,
            Login = admin.Login
        };
    }

    public async Task<AdministradorReadDto?> CreateAsync(AdministradorCreateDto dto)
    {
        if (dto.Senha.Length < 6)
            throw new ArgumentException("A senha deve ter no mínimo 6 caracteres.");

        var loginExiste = await _context.Administradores.AnyAsync(a => a.Login == dto.Login);
        if (loginExiste) return null;

        var admin = new Administrador
        {
            Login = dto.Login,
            Senha = BCrypt.Net.BCrypt.HashPassword(dto.Senha)
        };

        _context.Administradores.Add(admin);
        await _context.SaveChangesAsync();

        return new AdministradorReadDto
        {
            IdAdmin = admin.IdAdmin,
            Login = admin.Login
        };
    }
}