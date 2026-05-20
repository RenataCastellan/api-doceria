using api_doceria.DataContexts;
using api_doceria.Dtos;
using api_doceria.Models;
using Microsoft.EntityFrameworkCore;

namespace api_doceria.Services
{
    public class PedidoService
    {
        private readonly AppDbContext _context;

        public PedidoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PedidoReadDto>> GetAllAsync()
        {
            return await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Itens).ThenInclude(i => i.Produto)
                .Select(p => new PedidoReadDto
                {
                    IdPedido = p.IdPedido,
                    Data = p.Data,
                    Status = p.Status,
                    Total = p.Total,
                    IdCliente = p.IdCliente,
                    NomeCliente = p.Cliente!.Nome,
                    Itens = p.Itens.Select(i => new ItemPedidoReadDto
                    {
                        IdItem = i.IdItem,
                        NomeProduto = i.Produto!.Nome,
                        Quantidade = i.Quantidade,
                        PrecoUnitario = i.PrecoUnitario,
                        Subtotal = i.Quantidade * i.PrecoUnitario
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<PedidoReadDto?> GetByIdAsync(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Itens).ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(p => p.IdPedido == id);

            if (pedido == null) return null;

            return new PedidoReadDto
            {
                IdPedido = pedido.IdPedido,
                Data = pedido.Data,
                Status = pedido.Status,
                Total = pedido.Total,
                IdCliente = pedido.IdCliente,
                NomeCliente = pedido.Cliente!.Nome,
                Itens = pedido.Itens.Select(i => new ItemPedidoReadDto
                {
                    IdItem = i.IdItem,
                    NomeProduto = i.Produto!.Nome,
                    Quantidade = i.Quantidade,
                    PrecoUnitario = i.PrecoUnitario,
                    Subtotal = i.Quantidade * i.PrecoUnitario
                }).ToList()
            };
        }

        public async Task<PedidoReadDto?> CreateAsync(PedidoCreateDto dto)
        {
            var cliente = await _context.Clientes.FindAsync(dto.IdCliente);
            var endereco = await _context.Enderecos.FindAsync(dto.IdEndereco);
            if (cliente == null || endereco == null) return null;

            var pedido = new Pedido
            {
                IdCliente = dto.IdCliente,
                IdEndereco = dto.IdEndereco,
                Status = "Pendente"
            };

            decimal total = 0;
            foreach (var itemDto in dto.Itens)
            {
                var produto = await _context.Produtos.FindAsync(itemDto.IdProduto);
                if (produto == null || !produto.Ativo) continue;

                var item = new ItemPedido
                {
                    IdProduto = itemDto.IdProduto,
                    Quantidade = itemDto.Quantidade,
                    PrecoUnitario = produto.Preco
                };
                pedido.Itens.Add(item);
                total += item.Quantidade * item.PrecoUnitario;
            }

            pedido.Total = total;
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(pedido.IdPedido);
        }

        public async Task<bool> AtualizarStatusAsync(int id, string status)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null) return false;

            pedido.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
