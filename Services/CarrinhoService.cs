using api_doceria.DataContexts;
using api_doceria.Dtos;
using api_doceria.Models;
using Microsoft.EntityFrameworkCore;

namespace api_doceria.Services;

public class CarrinhoService
{
    private readonly AppDbContext _context;

    public CarrinhoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CarrinhoReadDto?> GetByIdAsync(int id)
    {
        var carrinho = await _context.Carrinhos
            .Include(c => c.Itens).ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(c => c.IdCarrinho == id);

        if (carrinho == null) return null;

        return MapToDto(carrinho);
    }

    public async Task<CarrinhoReadDto?> CreateAsync(int idCliente)
    {
        var clienteExiste = await _context.Clientes.AnyAsync(c => c.IdCliente == idCliente);
        if (!clienteExiste) return null;

        var carrinho = new Carrinho { IdCliente = idCliente };
        _context.Carrinhos.Add(carrinho);
        await _context.SaveChangesAsync();

        return MapToDto(carrinho);
    }

    public async Task<CarrinhoReadDto?> AdicionarItemAsync(ItemCarrinhoCreateDto dto)
    {
        var carrinho = await _context.Carrinhos
            .Include(c => c.Itens).ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(c => c.IdCarrinho == dto.IdCarrinho);

        if (carrinho == null) return null;

        var produto = await _context.Produtos.FindAsync(dto.IdProduto);
        if (produto == null || !produto.Ativo) return null;

        // Se o produto já está no carrinho, só aumenta a quantidade
        var itemExistente = carrinho.Itens.FirstOrDefault(i => i.IdProduto == dto.IdProduto);
        if (itemExistente != null)
        {
            itemExistente.Quantidade += dto.Quantidade;
        }
        else
        {
            carrinho.Itens.Add(new ItemCarrinho
            {
                IdCarrinho = dto.IdCarrinho,
                IdProduto = dto.IdProduto,
                Quantidade = dto.Quantidade
            });
        }

        await _context.SaveChangesAsync();

        // Recarrega com includes atualizados
        carrinho = await _context.Carrinhos
            .Include(c => c.Itens).ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(c => c.IdCarrinho == dto.IdCarrinho);

        return MapToDto(carrinho!);
    }

    public async Task<bool> RemoverItemAsync(int idItem)
    {
        var item = await _context.ItensCarrinho.FindAsync(idItem);
        if (item == null) return false;

        _context.ItensCarrinho.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }

    private CarrinhoReadDto MapToDto(Carrinho carrinho)
    {
        var itens = carrinho.Itens.Select(i => new ItemCarrinhoReadDto
        {
            IdItem = i.IdItem,
            NomeProduto = i.Produto?.Nome ?? "",
            PrecoProduto = i.Produto?.Preco ?? 0,
            Quantidade = i.Quantidade,
            Subtotal = i.Quantidade * (i.Produto?.Preco ?? 0)
        }).ToList();

        return new CarrinhoReadDto
        {
            IdCarrinho = carrinho.IdCarrinho,
            IdCliente = carrinho.IdCliente,
            CreatedAt = carrinho.CreatedAt,
            Itens = itens,
            Total = itens.Sum(i => i.Subtotal)
        };
    }
}