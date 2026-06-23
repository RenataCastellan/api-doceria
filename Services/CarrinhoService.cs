using api_doceria.DataContexts;
using api_doceria.Dtos;
using api_doceria.Exceptions;
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

    public async Task<CarrinhoReadDto> GetByIdAsync(int id)
    {
        var carrinho = await _context.Carrinhos
            .Include(c => c.Itens).ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(c => c.IdCarrinho == id);

        if (carrinho == null)
            throw new ErrorServiceException(
                $"Carrinho #{id} não encontrado",
                c => c.NotFound(new { mensagem = $"Carrinho #{id} não encontrado." }));

        return MapToDto(carrinho);
    }

    public async Task<CarrinhoReadDto> CreateAsync(int idCliente)
    {
        var clienteExiste = await _context.Clientes.AnyAsync(c => c.IdCliente == idCliente);
        if (!clienteExiste)
            throw new ErrorServiceException(
                $"Cliente #{idCliente} não encontrado",
                c => c.NotFound(new { mensagem = $"Cliente #{idCliente} não encontrado." }));

        var carrinho = new Carrinho { IdCliente = idCliente };
        _context.Carrinhos.Add(carrinho);
        await _context.SaveChangesAsync();

        return MapToDto(carrinho);
    }

    public async Task<CarrinhoReadDto> AdicionarItemAsync(ItemCarrinhoCreateDto dto)
    {
        var carrinho = await _context.Carrinhos
            .Include(c => c.Itens).ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(c => c.IdCarrinho == dto.IdCarrinho);

        if (carrinho == null)
            throw new ErrorServiceException(
                $"Carrinho #{dto.IdCarrinho} não encontrado",
                c => c.NotFound(new { mensagem = $"Carrinho #{dto.IdCarrinho} não encontrado." }));

        var produto = await _context.Produtos.FindAsync(dto.IdProduto);
        if (produto == null || !produto.Ativo)
            throw new ErrorServiceException(
                $"Produto #{dto.IdProduto} não encontrado ou inativo",
                c => c.NotFound(new { mensagem = $"Produto #{dto.IdProduto} não encontrado ou inativo." }));

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

        carrinho = await _context.Carrinhos
            .Include(c => c.Itens).ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(c => c.IdCarrinho == dto.IdCarrinho);

        return MapToDto(carrinho!);
    }

    public async Task RemoverItemAsync(int idItem)
    {
        var item = await _context.ItensCarrinho.FindAsync(idItem);

        if (item == null)
            throw new ErrorServiceException(
                $"Item #{idItem} não encontrado",
                c => c.NotFound(new { mensagem = $"Item #{idItem} não encontrado no carrinho." }));

        _context.ItensCarrinho.Remove(item);
        await _context.SaveChangesAsync();
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