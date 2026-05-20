namespace api_doceria.Models
{
    public class Pagamento
    {
        public int IdPagamento { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Status { get; set; } = "Pendente";
        public decimal Valor { get; set; }
        public DateTime DataPagamento { get; set; } = DateTime.Now;

        public int IdPedido { get; set; }
        public Pedido? Pedido { get; set; }
    }
}
