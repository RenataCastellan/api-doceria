using api_doceria.DataContexts;
using api_doceria.Dtos;
using api_doceria.Models;
using Microsoft.EntityFrameworkCore;

namespace api_doceria.Services;

public class PagamentoService
{
    private readonly AppDbContext _context;

    public PagamentoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PagamentoReadDto>> GetByPedidoAsync(int idPedido)
    {
        return await _context.Pagamentos
            .Where(p => p.IdPedido == idPedido)
            .Select(p => new PagamentoReadDto
            {
                IdPagamento = p.IdPagamento,
                Tipo = p.Tipo,
                Status = p.Status,
                Valor = p.Valor,
                DataPagamento = p.DataPagamento,
                IdPedido = p.IdPedido
            }).ToListAsync();
    }

    public async Task<PagamentoReadDto?> CreateAsync(PagamentoCreateDto dto)
    {
        var pedidoExiste = await _context.Pedidos.AnyAsync(p => p.IdPedido == dto.IdPedido);
        if (!pedidoExiste) return null;

        var pagamento = new Pagamento
        {
            Tipo = dto.Tipo,
            Valor = dto.Valor,
            Status = "Confirmado",
            IdPedido = dto.IdPedido
        };

        _context.Pagamentos.Add(pagamento);
        await _context.SaveChangesAsync();

        return new PagamentoReadDto
        {
            IdPagamento = pagamento.IdPagamento,
            Tipo = pagamento.Tipo,
            Status = pagamento.Status,
            Valor = pagamento.Valor,
            DataPagamento = pagamento.DataPagamento,
            IdPedido = pagamento.IdPedido
        };
    }
}