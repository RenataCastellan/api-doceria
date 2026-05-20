namespace api_doceria.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Endereco> Enderecos { get; set; } = new List<Endereco>();
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
        public ICollection<Carrinho> Carrinhos { get; set; } = new List<Carrinho>();
    }
}
