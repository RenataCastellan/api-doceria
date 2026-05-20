namespace api_doceria.Dtos
{
    public class PedidoCreateDto
    {
        public int IdCliente { get; set; }
        public int IdEndereco { get; set; }
        public List<ItemPedidoCreateDto> Itens { get; set; } = new();
    }

    public class ItemPedidoCreateDto
    {
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
    }

    public class PedidoReadDto
    {
        public int IdPedido { get; set; }
        public DateTime Data { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public int IdCliente { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
        public List<ItemPedidoReadDto> Itens { get; set; } = new();
    }

    public class ItemPedidoReadDto
    {
        public int IdItem { get; set; }
        public string NomeProduto { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}
