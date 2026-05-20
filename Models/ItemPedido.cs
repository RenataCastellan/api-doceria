namespace api_doceria.Models
{
    public class ItemPedido
    {
        public int IdItem { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }

        public int IdPedido { get; set; }
        public Pedido? Pedido { get; set; }

        public int IdProduto { get; set; }
        public Produto? Produto { get; set; }
    }
}
