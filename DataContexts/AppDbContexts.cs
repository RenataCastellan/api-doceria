using api_doceria.Models;
using Microsoft.EntityFrameworkCore;

namespace api_doceria.DataContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Administrador> Administradores { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<ItemPedido> ItensPedido { get; set; }
    public DbSet<Pagamento> Pagamentos { get; set; }
    public DbSet<Carrinho> Carrinhos { get; set; }
    public DbSet<ItemCarrinho> ItensCarrinho { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Chaves primárias explícitas
        modelBuilder.Entity<Administrador>().HasKey(a => a.IdAdmin);
        modelBuilder.Entity<Cliente>().HasKey(c => c.IdCliente);
        modelBuilder.Entity<Endereco>().HasKey(e => e.IdEndereco);
        modelBuilder.Entity<Produto>().HasKey(p => p.IdProduto);
        modelBuilder.Entity<Pedido>().HasKey(p => p.IdPedido);
        modelBuilder.Entity<ItemPedido>().HasKey(ip => ip.IdItem);
        modelBuilder.Entity<Pagamento>().HasKey(pg => pg.IdPagamento);
        modelBuilder.Entity<Carrinho>().HasKey(c => c.IdCarrinho);
        modelBuilder.Entity<ItemCarrinho>().HasKey(ic => ic.IdItem);

        // Cliente → Endereco (1:N)
        modelBuilder.Entity<Endereco>()
            .HasOne(e => e.Cliente)
            .WithMany(c => c.Enderecos)
            .HasForeignKey(e => e.IdCliente);

        // Cliente → Pedido (1:N)
        modelBuilder.Entity<Pedido>()
            .HasOne(p => p.Cliente)
            .WithMany(c => c.Pedidos)
            .HasForeignKey(p => p.IdCliente);

        // Endereco → Pedido (1:N)
        modelBuilder.Entity<Pedido>()
            .HasOne(p => p.Endereco)
            .WithMany(e => e.Pedidos)
            .HasForeignKey(p => p.IdEndereco)
            .OnDelete(DeleteBehavior.Restrict);

        // Cliente → Carrinho (1:N)
        modelBuilder.Entity<Carrinho>()
            .HasOne(c => c.Cliente)
            .WithMany(cl => cl.Carrinhos)
            .HasForeignKey(c => c.IdCliente);

        // Pedido → Pagamento (1:N)
        modelBuilder.Entity<Pagamento>()
            .HasOne(pg => pg.Pedido)
            .WithMany(p => p.Pagamentos)
            .HasForeignKey(pg => pg.IdPedido);

        // Administrador → Produto (1:N)
        modelBuilder.Entity<Produto>()
            .HasOne(pr => pr.Administrador)
            .WithMany(a => a.Produtos)
            .HasForeignKey(pr => pr.IdAdmin);

        // Pedido ↔ Produto via ItemPedido (N:N)
        modelBuilder.Entity<ItemPedido>()
            .HasOne(ip => ip.Pedido)
            .WithMany(p => p.Itens)
            .HasForeignKey(ip => ip.IdPedido);

        modelBuilder.Entity<ItemPedido>()
            .HasOne(ip => ip.Produto)
            .WithMany(pr => pr.ItensPedido)
            .HasForeignKey(ip => ip.IdProduto);

        // Carrinho ↔ Produto via ItemCarrinho (N:N)
        modelBuilder.Entity<ItemCarrinho>()
            .HasOne(ic => ic.Carrinho)
            .WithMany(c => c.Itens)
            .HasForeignKey(ic => ic.IdCarrinho);

        modelBuilder.Entity<ItemCarrinho>()
            .HasOne(ic => ic.Produto)
            .WithMany(pr => pr.ItensCarrinho)
            .HasForeignKey(ic => ic.IdProduto);

        // Precisão decimal
        modelBuilder.Entity<Produto>().Property(p => p.Preco).HasPrecision(10, 2);
        modelBuilder.Entity<ItemPedido>().Property(ip => ip.PrecoUnitario).HasPrecision(10, 2);
        modelBuilder.Entity<Pedido>().Property(p => p.Total).HasPrecision(10, 2);
        modelBuilder.Entity<Pagamento>().Property(pg => pg.Valor).HasPrecision(10, 2);
    }
}