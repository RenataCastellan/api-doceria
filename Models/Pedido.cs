namespace api_doceria.Models
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pendente";
        public decimal Total { get; set; }

        public int IdCliente { get; set; }
        public Cliente? Cliente { get; set; }

        public int IdEndereco { get; set; }
        public Endereco? Endereco { get; set; }

        public ICollection<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
        public ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();
    }
}
