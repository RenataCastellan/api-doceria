namespace api_doceria.Models
{
    public class Carrinho
    {
        public int IdCarrinho { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int IdCliente { get; set; }
        public Cliente? Cliente { get; set; }

        public ICollection<ItemCarrinho> Itens { get; set; } = new List<ItemCarrinho>();
    }
}
