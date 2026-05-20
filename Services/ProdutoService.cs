using api_doceria.DataContexts;
using api_doceria.Dtos;
using api_doceria.Models;
using Microsoft.EntityFrameworkCore;

namespace api_doceria.Services;

public class ProdutoService
{
    private readonly AppDbContext _context;

    public ProdutoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProdutoReadDto>> GetAllAsync(string? categoria = null)
    {
        var query = _context.Produtos.Where(p => p.Ativo);

        if (!string.IsNullOrEmpty(categoria))
            query = query.Where(p => p.Categoria == categoria);

        return await query.Select(p => new ProdutoReadDto
        {
            IdProduto = p.IdProduto,
            Nome = p.Nome,
            Descricao = p.Descricao,
            Preco = p.Preco,
            Imagem = p.Imagem,
            Categoria = p.Categoria,
            Estoque = p.Estoque,
            Ativo = p.Ativo
        }).ToListAsync();
    }

    public async Task<ProdutoReadDto?> GetByIdAsync(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null) return null;

        return new ProdutoReadDto
        {
            IdProduto = produto.IdProduto,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Preco = produto.Preco,
            Imagem = produto.Imagem,
            Categoria = produto.Categoria,
            Estoque = produto.Estoque,
            Ativo = produto.Ativo
        };
    }

    public async Task<ProdutoReadDto> CreateAsync(ProdutoCreateDto dto)
    {
        var produto = new Produto
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            Preco = dto.Preco,
            Imagem = dto.Imagem,
            Categoria = dto.Categoria,
            Estoque = dto.Estoque,
            Ativo = dto.Ativo,
            IdAdmin = dto.IdAdmin
        };

        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        return new ProdutoReadDto
        {
            IdProduto = produto.IdProduto,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Preco = produto.Preco,
            Imagem = produto.Imagem,
            Categoria = produto.Categoria,
            Estoque = produto.Estoque,
            Ativo = produto.Ativo
        };
    }

    public async Task<bool> UpdateAsync(int id, ProdutoUpdateDto dto)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null) return false;

        produto.Nome = dto.Nome;
        produto.Descricao = dto.Descricao;
        produto.Preco = dto.Preco;
        produto.Imagem = dto.Imagem;
        produto.Categoria = dto.Categoria;
        produto.Estoque = dto.Estoque;
        produto.Ativo = dto.Ativo;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null) return false;

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();
        return true;
    }
}