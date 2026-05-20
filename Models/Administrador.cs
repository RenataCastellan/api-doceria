namespace api_doceria.Models
{
    public class Administrador
    {
        public int IdAdmin { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;

        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
