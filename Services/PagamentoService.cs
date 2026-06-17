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

    private static readonly string[] TiposValidos = { "PIX", "Cartão de Crédito", "Cartão de Débito", "Dinheiro" };

    public async Task<PagamentoReadDto?> CreateAsync(PagamentoCreateDto dto)
    {
        if (!TiposValidos.Contains(dto.Tipo))
            throw new ArgumentException("Forma de pagamento inválida. Use: PIX, Cartão de Crédito, Cartão de Débito ou Dinheiro.");

        var pedido = await _context.Pedidos.FindAsync(dto.IdPedido);
        if (pedido == null) return null;

        var pagamento = new Pagamento
        {
            Tipo = dto.Tipo,
            Valor = dto.Valor,
            Status = "Confirmado",
            IdPedido = dto.IdPedido
        };

        _context.Pagamentos.Add(pagamento);
        pedido.Status = "Pago";

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