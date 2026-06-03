namespace api_doceria.Dtos;

public class CarrinhoReadDto
{
    public int IdCarrinho { get; set; }
    public int IdCliente { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<ItemCarrinhoReadDto> Itens { get; set; } = new();
    public decimal Total { get; set; }
}

public class ItemCarrinhoReadDto
{
    public int IdItem { get; set; }
    public string NomeProduto { get; set; } = string.Empty;
    public decimal PrecoProduto { get; set; }
    public int Quantidade { get; set; }
    public decimal Subtotal { get; set; }
}

public class ItemCarrinhoCreateDto
{
    public int IdCarrinho { get; set; }
    public int IdProduto { get; set; }
    public int Quantidade { get; set; }
}