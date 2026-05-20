namespace api_doceria.Models
{
    public class Endereco
    {
        public int IdEndereco { get; set; }
        public string Rua { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string? Complemento { get; set; }
        public string Bairro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string? Cep { get; set; }

        public int IdCliente { get; set; }
        public Cliente? Cliente { get; set; }

        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
