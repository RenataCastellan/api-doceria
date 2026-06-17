using api_doceria.DataContexts;
using api_doceria.Dtos;
using api_doceria.Models;
using Microsoft.EntityFrameworkCore;

namespace api_doceria.Services;

public class EnderecoService
{
    private readonly AppDbContext _context;

    public EnderecoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<EnderecoReadDto>> GetByClienteAsync(int idCliente)
    {
        return await _context.Enderecos
            .Where(e => e.IdCliente == idCliente)
            .Select(e => new EnderecoReadDto
            {
                IdEndereco = e.IdEndereco,
                Rua = e.Rua,
                Numero = e.Numero,
                Complemento = e.Complemento,
                Bairro = e.Bairro,
                Cidade = e.Cidade,
                Estado = e.Estado,
                Cep = e.Cep,
                IdCliente = e.IdCliente
            }).ToListAsync();
    }

    public async Task<EnderecoReadDto?> GetByIdAsync(int id)
    {
        var endereco = await _context.Enderecos.FindAsync(id);
        if (endereco == null) return null;

        return new EnderecoReadDto
        {
            IdEndereco = endereco.IdEndereco,
            Rua = endereco.Rua,
            Numero = endereco.Numero,
            Complemento = endereco.Complemento,
            Bairro = endereco.Bairro,
            Cidade = endereco.Cidade,
            Estado = endereco.Estado,
            Cep = endereco.Cep,
            IdCliente = endereco.IdCliente
        };
    }

    public async Task<EnderecoReadDto?> CreateAsync(EnderecoCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Rua) ||
            string.IsNullOrWhiteSpace(dto.Numero) ||
            string.IsNullOrWhiteSpace(dto.Bairro) ||
            string.IsNullOrWhiteSpace(dto.Cidade) ||
            string.IsNullOrWhiteSpace(dto.Estado))
        {
            throw new ArgumentException("Rua, número, bairro, cidade e estado são obrigatórios.");
        }

        var clienteExiste = await _context.Clientes.AnyAsync(c => c.IdCliente == dto.IdCliente);
        if (!clienteExiste) return null;

        var endereco = new Endereco
        {
            Rua = dto.Rua,
            Numero = dto.Numero,
            Complemento = dto.Complemento,
            Bairro = dto.Bairro,
            Cidade = dto.Cidade,
            Estado = dto.Estado,
            Cep = dto.Cep,
            IdCliente = dto.IdCliente
        };

        _context.Enderecos.Add(endereco);
        await _context.SaveChangesAsync();

        return new EnderecoReadDto
        {
            IdEndereco = endereco.IdEndereco,
            Rua = endereco.Rua,
            Numero = endereco.Numero,
            Complemento = endereco.Complemento,
            Bairro = endereco.Bairro,
            Cidade = endereco.Cidade,
            Estado = endereco.Estado,
            Cep = endereco.Cep,
            IdCliente = endereco.IdCliente
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var endereco = await _context.Enderecos.FindAsync(id);
        if (endereco == null) return false;

        _context.Enderecos.Remove(endereco);
        await _context.SaveChangesAsync();
        return true;
    }
}