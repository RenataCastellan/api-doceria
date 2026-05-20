namespace api_doceria.Models
{
    public class Produto
    {
        public int IdProduto { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public string? Imagem { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public int Estoque { get; set; }
        public bool Ativo { get; set; } = true;

        public int IdAdmin { get; set; }
        public Administrador? Administrador { get; set; }

        public ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
        public ICollection<ItemCarrinho> ItensCarrinho { get; set; } = new List<ItemCarrinho>();
    }
}
