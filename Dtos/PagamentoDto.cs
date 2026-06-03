namespace api_doceria.Dtos;

public class PagamentoCreateDto
{
    public string Tipo { get; set; } = string.Empty; // PIX, Cartão Crédito, Cartão Débito, Dinheiro
    public decimal Valor { get; set; }
    public int IdPedido { get; set; }
}

public class PagamentoReadDto
{
    public int IdPagamento { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public DateTime DataPagamento { get; set; }
    public int IdPedido { get; set; }
}