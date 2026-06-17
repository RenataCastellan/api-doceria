using api_doceria.DataContexts;
using api_doceria.Dtos;
using api_doceria.Models;
using Microsoft.EntityFrameworkCore;

namespace api_doceria.Services
{
    public class ClienteService
    {
        private readonly AppDbContext _context;

        public ClienteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClienteReadDto>> GetAllAsync()
        {
            return await _context.Clientes.Select(c => new ClienteReadDto
            {
                IdCliente = c.IdCliente,
                Nome = c.Nome,
                Email = c.Email,
                CreatedAt = c.CreatedAt
            }).ToListAsync();
        }

        public async Task<ClienteReadDto?> GetByIdAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return null;

            return new ClienteReadDto
            {
                IdCliente = cliente.IdCliente,
                Nome = cliente.Nome,
                Email = cliente.Email,
                CreatedAt = cliente.CreatedAt
            };
        }

        public async Task<ClienteReadDto?> CreateAsync(ClienteCreateDto dto)
        {
            // NF7.2 - senha com no mínimo 6 caracteres
            if (dto.Senha.Length < 6)
                throw new ArgumentException("A senha deve ter no mínimo 6 caracteres.");

            var emailExiste = await _context.Clientes.AnyAsync(c => c.Email == dto.Email);
            if (emailExiste) return null;

            var cliente = new Cliente
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = BCrypt.Net.BCrypt.HashPassword(dto.Senha)
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return new ClienteReadDto
            {
                IdCliente = cliente.IdCliente,
                Nome = cliente.Nome,
                Email = cliente.Email,
                CreatedAt = cliente.CreatedAt
            };
        }
    }
}
