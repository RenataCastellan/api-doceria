namespace api_doceria.Models
{
    public class ItemCarrinho
    {
        public int IdItem { get; set; }
        public int Quantidade { get; set; }

        public int IdCarrinho { get; set; }
        public Carrinho? Carrinho { get; set; }

        public int IdProduto { get; set; }
        public Produto? Produto { get; set; }
    }
}
